using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

[System.Serializable]
public class StoryJSON
{
    public string title_text;
    public string body_text;
}

[System.Serializable]
public class LocationJSON
{
    public double latitude;
    public double longitude;
    public string location_name;
    public List<StoryJSON> STORIES;
}

[System.Serializable]
public class LocationJSONList
{
    public List<LocationJSON> locations;
}

[System.Serializable]
public class LocationDataList
{
    public List<LocationData> locations;
}

public class LocationPlacer : MonoBehaviour
{
    [SerializeField] private AbstractMap _map;
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private float _spawnScale = 1f;
    [SerializeField] private float _fixedColliderRadius = 50f; // to be adjusted. 

    private readonly Vector3 y_increase = new Vector3(0, 10f, 0);

    private List<LocationJSON> _locationData = new List<LocationJSON>();
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    public GameObject spawnedObjectsHolder;

    void Start()
    {
        LoadLocations();
    }

    void Update()
    {
        // Keep markers positioned at correct map coords every frame
        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            var loc = _locationData[i];
            var latLon = new Vector2d(loc.latitude, loc.longitude);
            _spawnedObjects[i].transform.localPosition = _map.GeoToWorldPosition(latLon, true) + y_increase;
            //_spawnedObjects[i].transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);


            _spawnedObjects[i].transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

            // Fix collider to a constant world size regardless of zoom
            SphereCollider col = _spawnedObjects[i].GetComponent<SphereCollider>();
            if (col != null)
            {
                col.radius = _fixedColliderRadius / _spawnScale; // counteracts the scale
            }
        }
    }

    private void LoadLocations()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "locations.json");

        if (!File.Exists(path))
        {
            Debug.LogError("LocationPlacer: JSON file not found at " + path);
            return;
        }

        string json = File.ReadAllText(path);

        // Wrap raw array for JsonUtility
        string wrapped = $"{{\"locations\":{json}}}";
        LocationJSONList locationJSONList = JsonUtility.FromJson<LocationJSONList>(wrapped);


        if (locationJSONList == null || locationJSONList.locations == null)
        {
            Debug.LogError("LocationPlacer: Failed to parse JSON");
            return;
        }

        Debug.Log("LocationPlacer: loaded " + locationJSONList.locations.Count + " location(s).");

        for (int i = 0; i < locationJSONList.locations.Count; i++)
        {
            if (markerPrefab == null)
            {
                Debug.LogWarning("LocationPlacer: no prefab for location index " + i + ", skipping.");
                break;
            }

            LocationJSON data = locationJSONList.locations[i];

            // Spawn the prefab
            GameObject marker = Instantiate(markerPrefab, spawnedObjectsHolder.transform);
            var latLon = new Vector2d(data.latitude, data.longitude);
            marker.transform.localPosition = _map.GeoToWorldPosition(latLon, true) + y_increase;
            marker.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);


            // Wire up LocationData on the marker
            LocationMarker locationMarker = marker.GetComponent<LocationMarker>();
            if (locationMarker != null)
            {
                locationMarker.locationData.latitude = data.latitude;
                locationMarker.locationData.longitude = data.longitude;
                locationMarker.locationData.locationName = data.location_name;


                // Convert StoryJSON -> StoryObject
                locationMarker.locationData.storyList = new List<StoryCard>();
                foreach (StoryJSON s in data.STORIES)
                {
                    StoryCard story = new StoryCard
                    {
                        storyTitle = s.title_text,
                        storyBody = s.body_text
                    };
                    locationMarker.locationData.storyList.Add(story);
                }
            }

            _locationData.Add(data);
            _spawnedObjects.Add(marker);

        }
    }
}
