using UnityEngine;
using Mapbox.Utils;

public class LocationMarker : MonoBehaviour
{
    [SerializeField] public LocationData locationData;
    [SerializeField] private SpriteRenderer symbolRenderer;
    [SerializeField] public bool isInteractable = true;
    
    [Header("Discovery Settings")]
    public bool discovered = false;
    [SerializeField] private Color undiscoveredColor = Color.black;
    [SerializeField] private Color discoveredColor = Color.green;
    
    public LocationData LocationData => locationData;
    public Vector2d Coordinates => new Vector2d(locationData.latitude, locationData.longitude);

    private void Start()
    {
        if (symbolRenderer != null && locationData.symbol != null)
        {
            symbolRenderer.sprite = locationData.symbol;
            UpdateMarkerColor();
        }
    }

    private void UpdateMarkerColor()
    {
        Debug.Log($"Updating marker color IN LOCATION MARKER for {gameObject.name}. Discovered: {discovered}");
        // SpawnedObjectsHolder will always be the parent
        GameObject spawnedObjectsHolder = transform.parent.gameObject;
        if (spawnedObjectsHolder != null)
        {
            LocationPlacer locationPlacer = spawnedObjectsHolder.GetComponent<LocationPlacer>();
            if (locationPlacer != null)
            {
                locationPlacer.UpdateColor(this.gameObject, discovered ? discoveredColor : undiscoveredColor);
            }
            else
            {
                Debug.LogWarning("LocationPlacer component not found on parent.");
            }
        }
    }

    public void SetDiscovered(bool isDiscovered)
    {
        discovered = isDiscovered;
        UpdateMarkerColor();
    }

    void OnMouseDown()
    {

        if (MapMenuController.isMapMenuOpen) return;
        if (ModeController.isGoModeSelecting) return; 

        // If GO mode is on, only allow tap if player obj is overlapping this marker
        if (PlayerTrigger.instance != null && PlayerTrigger.instance.isGoMode)
        {
            if (!isInteractable) return;
        }

        PopupManager.instance.ShowLocationPopup(gameObject);
        SetDiscovered(true); // Mark as discovered
    }
}