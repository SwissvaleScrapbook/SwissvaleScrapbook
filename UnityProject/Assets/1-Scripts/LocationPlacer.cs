// from MAPBOX EXAMPLES 
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class LocationDataList
{
    public List<LocationData> locations;
}

public class LocationPlacer : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    float _spawnScale = 100f;

    // One prefab per location, in the same order as the JSON
    [SerializeField]
    List<GameObject> _markerPrefabs;

    // Name of JSON file inside StreamingAssets folder
    [SerializeField]
    string _jsonFileName = "swissvale_locations.json";

    List<LocationData> _locationData = new List<LocationData>();
    List<GameObject> _spawnedObjects = new List<GameObject>();

    void Start()
    {
        var data = LoadLocations();
        if (data == null || data.Count == 0)
        {
            Debug.LogWarning("LocationPlacer: no location data loaded.");
            return;
        }

        _locationData = data;

        for (int i = 0; i < _locationData.Count; i++)
        {
            if (i >= _markerPrefabs.Count)
            {
                Debug.LogWarning($"LocationPlacer: no prefab for location index {i}, skipping.");
                break;
            }

            var loc = _locationData[i];
            var latLon = new Vector2d(loc.latitude, loc.longitude);
            var instance = Instantiate(_markerPrefabs[i]);
            var y_increase = new Vector3(0, 10f, 0); // raise marker above ground to prevent z-fighting
            instance.transform.localPosition = _map.GeoToWorldPosition(latLon, true) + y_increase;
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            _spawnedObjects.Add(instance);
        }
    }

    void Update()
    {

        for (int i = 0; i < _spawnedObjects.Count; i++)
    {
        var loc = _locationData[i];
        var latLon = new Vector2d(loc.latitude, loc.longitude);
        var y_increase = new Vector3(0, 10f, 0);
        _spawnedObjects[i].transform.localPosition = _map.GeoToWorldPosition(latLon, true) + y_increase;
        _spawnedObjects[i].transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
    }
    }

// ---------------------------------------------------------------------------
    // DATA RETRIEVAL
    // SPRINT 2: reads from a local JSON file in StreamingAssets.
    // AFTER SUPABASE: replace this method body with a DB/API fetch.
    //         The rest of the class does not need to change.
    // ---------------------------------------------------------------------------
    List<LocationData> LoadLocations()
    {
        string path = Path.Combine(Application.streamingAssetsPath, _jsonFileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"LocationPlacer: JSON not found at {path}");
            return null;
        }

        string json = File.ReadAllText(path);
        var wrapper = JsonUtility.FromJson<LocationDataList>(json);

        if (wrapper == null || wrapper.locations == null)
        {
            Debug.LogError("LocationPlacer: failed to parse location JSON.");
            return null;
        }

        Debug.Log($"LocationPlacer: loaded {wrapper.locations.Count} location(s).");
        return wrapper.locations;
    }
}

