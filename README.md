# Swissvale Scrapbook
Created by Greyson Barsotti (gjb46@pitt.edu)

In Partnership with Project Sponsors:
- Dr. Susan Lucas
- Dr. Dawna Cerney
- Dr. Amy Flick

This project was created as a part of a senior capstone class at the University of Pittsburgh.

## What is Swissvale Scrapbook

**The Problem**: 

Researchers interested in Swissvale, PA, have collected data on the community relating to the vacant lots scattered around the borough. The statistics are being stored within software for geographic information systems. How can that data be combined with oral stories to make the average person interested and transition the community into a state of revitalization?

**The Solution**: 

To get people interested in the data, I designed Swissvale Scrapbook, an experience that takes users around the community of Swissvale. The experience is framed around oral histories about the vacant lots collected from Swissvale residents. This is a community-first application, so careful consideration has gone into decisions to ensure community wishes are respected and prevent gentrification.

The current version in this repository is a rough demo. Currently, you are able to add areas of interest (AOIs) within the Unity project, then have users walk towards the AOI and interact with it. 

Currently, the pop-up at each AOI shows users:
1. The AOI Title
2. Three pictures of the AOI
3. One "main anecdote" of the oral histories collected from the AOI
4. Six "sub anecdotes" of the oral histories from the AOI

To see the future features not yet implemented, see [Future Improvements](#future-improvements)

## Installing the Unity Project

1. Install [Unity Hub](https://unity.com/download)
2. Under installs, select Unity 6000.3.0f1
3. Ensure to select the proper build tools for your device you plan on deploying to
4. Clone this repository to your local machine
5. In Unity, under Projects, click Add
6. Select the UnityProject folder within the repository you cloned down

## Building the Project on iPhone

> NOTE: The current version of the project has only been tested on an iPhone. To deploy to other devices, refer to official Unity Documentation

1. Create a free Apple Developer account if you don't already have one. You don't need a full developer license, just an account.
2. Install [xcode](https://developer.apple.com/xcode/) from Apple's website.
3. Plug your phone into your computer via USB.
4. Ensure [developer mode](https://developer.apple.com/documentation/xcode/enabling-developer-mode-on-a-device) is enabled on your phone. You may need to restart your phone.
5. In Unity, go to File > Build Profiles.
6. Select "iOS" under platforms and ensure this is the active profile. If not, click "Switch Platform".
7. Ensure the "Location-basedGame" scene is selected in the Scene List.
8. Click "Build and Run".
9. Create a folder on your local machine to hold the build files. Ensure this is **not committed to the repository**.

If you get this (or a similar) error:

> Signing for "Unity-iPhone" requires a development team. Select a development team in the Signing & Capabilities editor.

1. Click on the error in xcode.
2. Ensure "Automatically manage signing" is enabled
3. Make a personal team for your developer account
4. Set the bundle indentifier to "com.swissvalescrapbook"

## Technologies Used

- Unity v6000.3.0f1
- Mapbox v2.0.1
- Niantic Lightship AR Plugin v3.16.0-2509150829

## <a name="future-improvements"></a>Future Improvements


