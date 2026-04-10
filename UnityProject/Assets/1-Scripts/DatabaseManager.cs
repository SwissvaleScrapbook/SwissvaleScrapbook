using UnityEngine;
using System.Net;
using System;
using System.IO;
using Assets;
using System.Collections.Generic;


public class DatabaseManager : MonoBehaviour
{
    private const string url = "https://ocztrljoyaeqaqlwccxr.supabase.co/rest/v1";
    
    // api key CAN BE PUBLISHED/COMMITTED since it's a public key with only read permissions
    private const string api_key = "sb_publishable_olBgAYDidISMbgSfrhM0Qg_R-ul3Wgw";
    private List<LocationData> locationDataList = new List<LocationData>();

    public void Start()
    {
        getLocations();
    }

    private void getLocations()
    {
        // Get locations and put them into a json in the StreamingAssets folder
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/LOCATIONMARKERS?apikey={1}&select=*,STORIES(*)", url, api_key));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            string json = reader.ReadToEnd();
            File.WriteAllText(Application.streamingAssetsPath + "/locations.json", json);
        }
    }
    
    // Check if the images from the database are already downloaded 
    private void checkLocations()
    {
        // Read the images json file from the StreamingAssets folder
        string json = File.ReadAllText(Application.streamingAssetsPath + "/databaseImages/imageInfo.json");

        // Check the imageCount in the json file
        int localImageCount = JsonUtility.FromJson<ImageInfo>(json).imageCount;

        // Fetch the max image id from the database
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/IMAGES?apikey={1}&select=image_id&order=image_id.desc&limit=1", url, api_key));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            string responseJson = reader.ReadToEnd();
            int maxImageId = JsonUtility.FromJson<ImageIdResponse>(responseJson).image_id;

            // If the local image count is less than the max image id, download the new images
            if (localImageCount < maxImageId)
            {
                downloadImages(localImageCount + 1, maxImageId);
            }
        }
    }

    public void downloadImages()
    {
        // Delete all images frrom the local databaseImages folder
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/databaseImages");
        foreach (string file in files)
        {
            File.Delete(file);
        }

        // Fetch all images from the database
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/IMAGES?apikey={1}", url, api_key));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        // For each image in the response, download the image from the bucket and save it to the local databaseImages folder
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            // https://ocztrljoyaeqaqlwccxr.supabase.co/storage/v1/object/public/Images/[image_name]
        }
        
    }

}
