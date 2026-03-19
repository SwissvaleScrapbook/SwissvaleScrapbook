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
    [SerializeField] private List<GameObject> _markerPrefabs;

    private Vector3 y_increase = new Vector3(0, 10f, 0);

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
        if (i >= _markerPrefabs.Count)
        {
            Debug.LogWarning("LocationPlacer: More locations in JSON than prefabs!");
            break;
        }

        LocationJSON data = locationJSONList.locations[i];
        GameObject marker = Instantiate(_markerPrefabs[i], transform);
        LocationMarker locationMarker = marker.GetComponent<LocationMarker>();

        if (locationMarker != null)
        {
            locationMarker.locationData.latitude = data.latitude;
            locationMarker.locationData.longitude = data.longitude;
            locationMarker.locationData.locationName = data.locationName;

            // Convert StoryJSON to StoryObject
            locationMarker.locationData.stories = new List<StoryObject>();
            foreach (StoryJSON s in data.stories)
            {
                StoryObject story = new StoryObject();
                story.title = s.title;
                story.body = s.body;
                locationMarker.locationData.stories.Add(story);
            }
        }
    }
}

      
}
