using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NianticSpatial.NSDK.AR.WorldPositioning;

public class AddWPSObjects : MonoBehaviour
{
    [SerializeField] ARWorldPositioningObjectHelper positioningHelper;

    // Start is called before the first frame update
    void Start()
    {
        // replace the coordinates here with your location
        double latitude = 37.79534850764306;
        double longitude = -122.39243231803636;
        double altitude = 0.0; // We're using camera-relative positioning so make the cube appear at the same height as the camera

        // instantiate a cube, scale it up for visibility (make it even bigger if you need), then update its location
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale *= 2.0f;
        positioningHelper.AddOrUpdateObject(cube, latitude, longitude, altitude, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}