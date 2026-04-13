using UnityEngine;
using System.Net;
using System;
using System.IO;
using Assets;
using System.Collections.Generic;

[System.Serializable]
public class dbInfo
{
    public string updated_at;
}

[System.Serializable]
public class dbInfoList { public List<dbInfo> items; }

[System.Serializable]
public class ImageJSON
{
    public int image_id;
    public string image_name;
}

[System.Serializable]
public class ImageJSONList { public List<ImageJSON> images; }

public class DatabaseManager : MonoBehaviour
{
    private const string url = "https://ocztrljoyaeqaqlwccxr.supabase.co/rest/v1";
    private const string bucketUrl = "https://ocztrljoyaeqaqlwccxr.supabase.co/storage/v1/object/public/Images";

    // api key CAN BE PUBLISHED/COMMITTED since it's a public key with only read permissions
    private const string api_key = "sb_publishable_olBgAYDidISMbgSfrhM0Qg_R-ul3Wgw";
    private dbInfo localDbInfo = new dbInfo();
    private dbInfo remoteDbInfo = new dbInfo();

    public void Start()
    {
        checkLocalInfo();
        checkRemoteInfo();

        // If the remote update time is more recent than the local update time, then we need to download the images
        if (DateTime.Parse(remoteDbInfo.updated_at) > DateTime.Parse(localDbInfo.updated_at))
        {
            deleteImages();
            downloadImages();
            updateJson();
        }
        else{
            Debug.Log("Start: Local images are up to date. No download needed.");
        }

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
    private void checkLocalInfo()
    {

        // CHeck if the databaseInformation.json file exists in the StreamingAssets folder
        if (!File.Exists(Application.streamingAssetsPath + "/databaseImages/databaseInformation.json"))
        {
            Debug.Log("checkLocalInfo: databaseInformation.json not found. Assuming no local images.");
            localDbInfo.updated_at = "1970-01-01T00:00:00Z"; // Set to epoch time to force download
            return;
        }

        // Read the images json file from the StreamingAssets folder
        string json = File.ReadAllText(Application.streamingAssetsPath + "/databaseImages/databaseInformation.json");

        // Check updateTime and save it
        localDbInfo.updated_at = JsonUtility.FromJson<dbInfo>(json).updated_at;
    }

    private void checkRemoteInfo()
    {
        // Grab update time by querying table where table_name = IMAGES
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/LASTUPDATED?apikey={1}&table_name=eq.IMAGES&select=updated_at", url, api_key));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            string json = reader.ReadToEnd();
            Debug.Log("checkRemoteInfo raw response: " + json);
            string wrapped = String.Format("{{\"items\":{0}}}", json);
            dbInfo parsed = JsonUtility.FromJson<dbInfoList>(wrapped).items[0];
            remoteDbInfo.updated_at = parsed.updated_at;
        }
    }

    public void deleteImages()
    {
        // Delete all images frrom the local databaseImages folder
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/databaseImages");
        foreach (string file in files)
        {
            File.Delete(file);
        }
    }

    public void downloadImages()
    {
        // Fetch all images from the database
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/IMAGES?apikey={1}&select=*", url, api_key));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        string json;
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            json = reader.ReadToEnd();
        }

        // Wrap and parse
        string wrapped = String.Format("{{\"images\":{0}}}", json);
        ImageJSONList imageList = JsonUtility.FromJson<ImageJSONList>(wrapped);

        if (imageList == null || imageList.images == null)
        {
            Debug.LogError("downloadImages: Failed to parse image list");
            return;
        }

        // Download each image from the bucket
        foreach (ImageJSON img in imageList.images)
        {
            string saveFolder = Path.Combine(Application.streamingAssetsPath, "databaseImages");
            string imageUrl = String.Format("{0}/{1}", bucketUrl, img.image_name);
            string savePath = Path.Combine(saveFolder, img.image_id + ".png");

            try
            {
                HttpWebRequest imgRequest = (HttpWebRequest)WebRequest.Create(imageUrl);

                using (HttpWebResponse imgResponse = (HttpWebResponse)imgRequest.GetResponse())
                using (Stream imgStream = imgResponse.GetResponseStream())
                using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    imgStream.CopyTo(fileStream);
                }

                Debug.Log(String.Format("downloadImages: Saved image {0} to {1}", img.image_id, savePath));
            }
            catch (Exception e)
            {
                Debug.LogError(String.Format("downloadImages: Failed to download image {0}: {1}", img.image_id, e.Message));
            }
        }

        Debug.Log(String.Format("downloadImages: Done. {0} image(s) downloaded.", imageList.images.Count));
    }

    private void updateJson()
    {
        // Update the databaseInformation.json file with the new update time
        localDbInfo.updated_at = remoteDbInfo.updated_at;
        string json = JsonUtility.ToJson(localDbInfo);
        File.WriteAllText(Application.streamingAssetsPath + "/databaseImages/databaseInformation.json", json);
    }

}
