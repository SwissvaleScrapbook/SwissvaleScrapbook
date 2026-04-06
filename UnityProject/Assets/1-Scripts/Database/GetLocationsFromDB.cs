using UnityEngine;
using System.Net;
using System;
using System.IO;
using Assets;
using System.Collections.Generic;


public class GetLocationsFromDB : MonoBehaviour
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
    

}
