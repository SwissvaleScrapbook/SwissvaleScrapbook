using UnityEngine;
using Mapbox.Utils;

public class LocationMarker : MonoBehaviour
{
    [SerializeField] public LocationData locationData;
    [SerializeField] private SpriteRenderer symbolRenderer;
    
    [Header("Discovery Settings")]
    public bool discovered = false;
    [SerializeField] private Color undiscoveredColor = Color.gray;
    [SerializeField] private Color discoveredColor = Color.green;
    
    public LocationData LocationData => locationData;
    public Vector2d Coordinates => new Vector2d(locationData.latitude, locationData.longitude);

    private void Start()
    {
        if (symbolRenderer != null && locationData.symbol != null)
        {
            symbolRenderer.sprite = locationData.symbol;
        }
        
        UpdateMarkerColor();
    }

    public void SetDiscovered(bool isDiscovered)
    {
        discovered = isDiscovered;
        UpdateMarkerColor();
    }

    private void UpdateMarkerColor()
    {
        if (symbolRenderer != null)
        {
            symbolRenderer.color = discovered ? discoveredColor : undiscoveredColor;
        }
    }

}