using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NianticSpatial.NSDK.AR.WorldPositioning;
using UnityEngine.XR.ARFoundation;
using Mapbox.Utils;
using Mapbox.Unity.Location;

public class WPSCompassProvider : MonoBehaviour
{
    [SerializeField] public ARWorldPositioningManager positioningManager;

    public ARWorldPositioningCameraHelper _WPSCameraHelper;

    public float heading;
    public double latitude;
    public double longitude;
    public double altitude;
    public Vector2d LatitudeLongitude;
    Quaternion q;
    public Location _currentLocation;

    // Start is called before the first frame update
    void Start()
    {
        _WPSCameraHelper = positioningManager.GetComponent<ARWorldPositioningCameraHelper>();
    }

    // Update is called once per frame
    void Update()
    {
        heading = _WPSCameraHelper.TrueHeading;

        latitude = _WPSCameraHelper.Latitude;
        longitude = _WPSCameraHelper.Longitude;
        LatitudeLongitude = new Vector2d(latitude, longitude);
        altitude = _WPSCameraHelper.Altitude;

        _currentLocation.LatitudeLongitude = LatitudeLongitude;

        q = _WPSCameraHelper.RotationCameraRUFToWorldEUN;
    }
}