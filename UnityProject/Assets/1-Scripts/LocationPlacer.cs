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
    public int symbol_id;
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

    private static readonly Dictionary<int, string> symbolNames = new Dictionary<int, string>
    {
        { 0, "aerialway" }, { 1, "airfield" }, { 2, "airport" }, { 3, "alcohol-shop" },
        { 4, "american-football" }, { 5, "amusement-park" }, { 6, "animal-shelter" },
        { 7, "aquarium" }, { 8, "arrow" }, { 9, "art-gallery" }, { 10, "attraction" },
        { 11, "bakery" }, { 12, "bank" }, { 13, "bank-JP" }, { 14, "bar" },
        { 15, "barrier" }, { 16, "baseball" }, { 17, "basketball" }, { 18, "bbq" },
        { 19, "beach" }, { 20, "beer" }, { 21, "bicycle" }, { 22, "bicycle-share" },
        { 23, "blood-bank" }, { 24, "bowling-alley" }, { 25, "bridge" }, { 26, "building" },
        { 27, "building-alt1" }, { 28, "bus" }, { 29, "cafe" }, { 30, "campsite" },
        { 31, "car" }, { 32, "car-rental" }, { 33, "car-repair" }, { 34, "casino" },
        { 35, "castle" }, { 36, "castle-JP" }, { 37, "caution" }, { 38, "cemetery" },
        { 39, "cemetery-JP" }, { 40, "charging-station" }, { 41, "cinema" }, { 42, "circle" },
        { 43, "circle-stroked" }, { 44, "city" }, { 45, "clothing-store" }, { 46, "college" },
        { 47, "college-JP" }, { 48, "commercial" }, { 49, "communications-tower" },
        { 50, "confectionery" }, { 51, "construction" }, { 52, "convenience" }, { 53, "cricket" },
        { 54, "cross" }, { 55, "dam" }, { 56, "danger" }, { 57, "defibrillator" },
        { 58, "dentist" }, { 59, "diamond" }, { 60, "doctor" }, { 61, "dog-park" },
        { 62, "drinking-water" }, { 63, "elevator" }, { 64, "embassy" }, { 65, "emergency-phone" },
        { 66, "entrance" }, { 67, "entrance-alt1" }, { 68, "farm" }, { 69, "fast-food" },
        { 70, "fence" }, { 71, "ferry" }, { 72, "ferry-JP" }, { 73, "fire-station" },
        { 74, "fire-station-JP" }, { 75, "fitness-centre" }, { 76, "florist" }, { 77, "fuel" },
        { 78, "furniture" }, { 79, "gaming" }, { 80, "garden" }, { 81, "garden-centre" },
        { 82, "gate" }, { 83, "gift" }, { 84, "globe" }, { 85, "golf" }, { 86, "grocery" },
        { 87, "hairdresser" }, { 88, "harbor" }, { 89, "hardware" }, { 90, "heart" },
        { 91, "heliport" }, { 92, "highway-rest-area" }, { 93, "historic" }, { 94, "home" },
        { 95, "horse-riding" }, { 96, "hospital" }, { 97, "hospital-JP" }, { 98, "hot-spring" },
        { 99, "ice-cream" }, { 100, "industry" }, { 101, "information" }, { 102, "jewelry-store" },
        { 103, "karaoke" }, { 104, "landmark" }, { 105, "landmark-JP" }, { 106, "landuse" },
        { 107, "laundry" }, { 108, "library" }, { 109, "lift-gate" }, { 110, "lighthouse" },
        { 111, "lighthouse-JP" }, { 112, "lodging" }, { 113, "logging" }, { 114, "marker" },
        { 115, "marker-stroked" }, { 116, "mobile-phone" }, { 117, "monument" },
        { 118, "monument-JP" }, { 119, "mountain" }, { 120, "museum" }, { 121, "music" },
        { 122, "natural" }, { 123, "observation-tower" }, { 124, "optician" }, { 125, "paint" },
        { 126, "park" }, { 127, "park-alt1" }, { 128, "parking" }, { 129, "parking-garage" },
        { 130, "parking-paid" }, { 131, "pharmacy" }, { 132, "picnic-site" }, { 133, "pitch" },
        { 134, "place-of-worship" }, { 135, "playground" }, { 136, "police" },
        { 137, "police-JP" }, { 138, "post" }, { 139, "post-JP" }, { 140, "prison" },
        { 141, "racetrack" }, { 142, "racetrack-boat" }, { 143, "racetrack-cycling" },
        { 144, "racetrack-horse" }, { 145, "rail" }, { 146, "rail-light" }, { 147, "rail-metro" },
        { 148, "ranger-station" }, { 149, "recycling" }, { 150, "religious-buddhist" },
        { 151, "religious-christian" }, { 152, "religious-jewish" }, { 153, "religious-muslim" },
        { 154, "religious-shinto" }, { 155, "residential-community" }, { 156, "restaurant" },
        { 157, "restaurant-bbq" }, { 158, "restaurant-noodle" }, { 159, "restaurant-pizza" },
        { 160, "restaurant-seafood" }, { 161, "restaurant-sushi" }, { 162, "road-accident" },
        { 163, "roadblock" }, { 164, "rocket" }, { 165, "school" }, { 166, "school-JP" },
        { 167, "scooter" }, { 168, "shelter" }, { 169, "shoe" }, { 170, "shop" },
        { 171, "skateboard" }, { 172, "skiing" }, { 173, "slaughterhouse" }, { 174, "slipway" },
        { 175, "snowmobile" }, { 176, "soccer" }, { 177, "square" }, { 178, "square-stroked" },
        { 179, "stadium" }, { 180, "star" }, { 181, "star-stroked" }, { 182, "suitcase" },
        { 183, "swimming" }, { 184, "table-tennis" }, { 185, "teahouse" }, { 186, "telephone" },
        { 187, "tennis" }, { 188, "theatre" }, { 189, "toilet" }, { 190, "toll" },
        { 191, "town" }, { 192, "town-hall" }, { 193, "triangle" }, { 194, "triangle-stroked" },
        { 195, "tunnel" }, { 196, "veterinary" }, { 197, "viewpoint" }, { 198, "village" },
        { 199, "volcano" }, { 200, "volleyball" }, { 201, "warehouse" }, { 202, "waste-basket" },
        { 203, "watch" }, { 204, "water" }, { 205, "waterfall" }, { 206, "watermill" },
        { 207, "wetland" }, { 208, "wheelchair" }, { 209, "windmill" }, { 210, "zoo" }
    };

    void Start()
    {
        LoadLocations();
    }

    void Update()
    {
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

    private Sprite LoadSymbol(int symbolId)
    {
        if (!symbolNames.TryGetValue(symbolId, out string name))
        {
            Debug.LogWarning($"LocationPlacer: No symbol name for id {symbolId}");
            name = "information"; // default symbol
        }

        Sprite sprite = Resources.Load<Sprite>($"all_maki_icons/svgs/{name}");

        if (sprite == null)
        {
            // Try loading the child sprite if the asset has sub-sprites
            Object[] sprites = Resources.LoadAll<Sprite>($"Symbols/{name}");
            if (sprites.Length > 0)
                sprite = sprites[0] as Sprite;
        }

        if (sprite == null)
            Debug.LogWarning($"LocationPlacer: Could not load sprite for symbol '{name}' (id {symbolId})");

        return sprite;
    }

    private void LoadLocations()
    {
        string path = Path.Combine(Application.persistentDataPath, "locations.json");

        if (!File.Exists(path))
        {
            Debug.LogError("LocationPlacer: JSON file not found at " + path);
            return;
        }

        string json = File.ReadAllText(path);
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

            GameObject marker = Instantiate(markerPrefab, spawnedObjectsHolder.transform);
            var latLon = new Vector2d(data.latitude, data.longitude);
            marker.transform.localPosition = _map.GeoToWorldPosition(latLon, true) + y_increase;
            marker.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

            LocationMarker locationMarker = marker.GetComponent<LocationMarker>();
            if (locationMarker != null)
            {
                locationMarker.locationData.latitude = data.latitude;
                locationMarker.locationData.longitude = data.longitude;
                locationMarker.locationData.locationName = data.location_name;
                locationMarker.locationData.symbol = LoadSymbol(data.symbol_id); // assign sprite

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