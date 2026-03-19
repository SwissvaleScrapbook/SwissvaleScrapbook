using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ImageObject
{
    public Sprite image;
    public string caption;
}

[Serializable]
public class StoryObject
{
    public string title;
    // adding
    public string body;
    public List<string> quotes = new List<string>();
    public List<AudioClip> audioFiles = new List<AudioClip>();    
}

[Serializable]
public class LocationData
{
    [Header("Location Settings")]
    public double latitude;
    public double longitude;
    
    [Header("Marker Display")]
    public Sprite symbol;
    public string locationName;
    
    [Header("Content")]
    public List<ImageObject> imgList = new List<ImageObject>();
    // adding
    public List<StoryObject> stories = new List<StoryObject>();
    public List<StoryCard> storyList = new List<StoryCard>();
}