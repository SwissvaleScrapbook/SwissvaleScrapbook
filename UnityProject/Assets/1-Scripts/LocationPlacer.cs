/*
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class StoryJSON
{
    public string title;
    public string body;
}

[System.Serializable]
public class LocationJSON
{
    public double latitude;
    public double longitude;
    public string locationName;
    public List<StoryJSON> stories;
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
    [SerializeField] private GameObject markerPrefab;

    private Vector3 y_increase = new Vector3(0, 10f, 0);


    List<GameObject> _spawnedObjects = new List<GameObject>();
    void Start()
    {
        LoadLocations();
    }

    void Update()
    {
        
        // Keep markers raised above ground
        foreach (Transform child in transform)
        {
            child.position = new Vector3(child.position.x, 10f, child.position.z);
        }
    }

    private void LoadLocations()
{
    string path = Path.Combine(Application.streamingAssetsPath, "swissvale_locations.json");

    if (!File.Exists(path))
    {
        Debug.LogError("LocationPlacer: JSON file not found at " + path);
        return;
    }

    string json = File.ReadAllText(path);
    LocationJSONList locationJSONList = JsonUtility.FromJson<LocationJSONList>(json);

    Debug.Log("Raw JSON: " + json);
    Debug.Log("Locations parsed: " + locationJSONList.locations.Count);
    Debug.Log("Stories in location 0: " + locationJSONList.locations[0].stories.Count);

    if (locationJSONList == null || locationJSONList.locations == null)
    {
        Debug.LogError("LocationPlacer: Failed to parse JSON");
        return;
    }

    for (int i = 0; i < locationJSONList.locations.Count; i++)
    {
        if (markerPrefab == null)
        {
            Debug.LogError("LocationPlacer: Marker prefab not assigned!");
            return;
        }

        LocationJSON data = locationJSONList.locations[i];
        GameObject marker = Instantiate(markerPrefab, transform);
        LocationMarker locationMarker = marker.GetComponent<LocationMarker>();

        if (locationMarker != null)
        {
            locationMarker.locationData.latitude = data.latitude;
            locationMarker.locationData.longitude = data.longitude;
            locationMarker.locationData.locationName = data.locationName;

            // Convert StoryJSON to StoryObject
            locationMarker.locationData.storyList = new List<StoryCard>();
            foreach (StoryJSON s in data.stories)
            {
                StoryCard story = new StoryCard();
                story.storyTitle = s.title;
                story.storyBody = s.body;
                locationMarker.locationData.storyList.Add(story);
            }
        }
    }
}

      
}
*/

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

[System.Serializable]
public class StoryJSON
{
    public string title;
    public string body;
}

[System.Serializable]
public class LocationJSON
{
    public double latitude;
    public double longitude;
    public string locationName;
    public List<StoryJSON> stories;
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
            _spawnedObjects[i].transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
    }

    private void LoadLocations()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "swissvale_locations.json");

        if (!File.Exists(path))
        {
            Debug.LogError("LocationPlacer: JSON file not found at " + path);
            return;
        }

        string json = File.ReadAllText(path);
        LocationJSONList locationJSONList = JsonUtility.FromJson<LocationJSONList>(json);

        if (locationJSONList == null || locationJSONList.locations == null)
        {
            Debug.LogError("LocationPlacer: Failed to parse JSON");
            return;
        }

        Debug.Log("LocationPlacer: loaded " + locationJSONList.locations.Count + " location(s).");

        for (int i = 0; i < locationJSONList.locations.Count; i++)
        {
            if (markerPrefab ==null)
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
                locationMarker.locationData.locationName = data.locationName;


                // Convert StoryJSON -> StoryObject
                locationMarker.locationData.storyList = new List<StoryCard>();
                foreach (StoryJSON s in data.stories)
                {
                    StoryCard story = new StoryCard();
                    story.storyTitle = s.title;
                    story.storyBody = s.body;
                    locationMarker.locationData.storyList.Add(story);
                }
            }

            _locationData.Add(data);
            _spawnedObjects.Add(marker);

        }
    }
}
