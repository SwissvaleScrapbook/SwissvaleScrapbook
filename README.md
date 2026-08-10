# Swissvale Scrapbook
Created by Greyson Barsotti (gjb46@pitt.edu)
Developed by Greyson Barsotti, Amy Zhang, and Nickhil Naranjan

In Partnership with Project Sponsors:
- Dr. Susan Lucas
- Dr. Dawna Cerney
- Dr. Amy Flick
- Dr. Jessica FitzPatrick

This project was created as a part of a senior capstone class at the University of Pittsburgh.

## What is Swissvale Scrapbook

**The Problem**: 

Researchers interested in Swissvale, PA, have collected data on the community relating to the vacant lots scattered around the borough. The statistics are being stored within software for geographic information systems. How can that data be combined with oral stories to make the average person interested and transition the community into a state of revitalization?

**The Solution**: 

To get people interested in the data, I designed Swissvale Scrapbook, an experience that takes users around the community of Swissvale. The experience is framed around oral histories about the vacant lots collected from Swissvale residents. This is a community-first application, so careful consideration has gone into decisions to ensure community wishes are respected and prevent gentrification.

Currently, the pop-up at each AOI shows users:
1. The AOI Title
2. Three pictures of the AOI
3. A stack of stories

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

## <a name="future-improvements"></a>Future Improvements

### AOI Pop-ups

- **Anecdotes**: Full interaction with oral history anecdotes. Currently they are buttons, but clicking them doesn't lead anywhere. In the future, they will be able to take users to a scrolling list of quotes, and if permissions allow, audio snippets from data walks.
- **Images**: Full interaction with the images at the bottom of the screen. Clicking on these images should expand into a scrolling list of images. Users should be able to overlay each image onto the current camera view to compare what the vacant lot was, and what it is today.
- **Style**: The current, simple style of the pop-up was made for the demo. In the future, the design of the elements in the pop-up will be improved and tweaked.

### Map Screen
- **Guidance**: Users can be guided to a nearby AOI, likely using Mapbox or custom routing capabilities.

### Co-design Sessions

In order to improve the functionality and design of the project, co-design sessions will be conducted. These will interface directly with the community of Swissvale to ensure that the project connects with them.

> Improvements to the project were made over the course of the semester using feedback from in-class workshop sessions, feedback from a classmate who is a resident of Swissvale, feedback from project sponsors, and feedback from the end of semester showcase. Further gathering feedback from Swissvalians will allow the project to cater more towards them.

### Accessibility
- **Tech Access**: Not everyone has access to a mobile phone or a cellular connection. This project should be able to be fully interactable in the Carnegie Free Library of Swissvale *at minimum*.

### Miscellaneous
- **Badges**: Implementing some sort of badge system to increase engagement. Users should get badges for completing various tasks, such as going to a certain number of AOIs.
- **Online Connectivity**: To increase connection within the community and promote collaboration, users should be able to connect with each other. This could be done as some sort of friend system.
- **Device Support**: Currently, the app only supports iPhones. In the future, this should be able to run on both iOS and Android devices. Additionally, users should be able to run this on desktops without using location data. Instead, they will be able to move the character around using keyboard controls or click on a AOI to investigate it.
