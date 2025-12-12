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

### AOI Pop-ups

- **Anecdotes**: Full interaction with oral history anecdotes. Currently they are buttons, but clicking them doesn't lead anywhere. In the future, they will be able to take users to a scrolling list of quotes, and if permissions allow, audio snippets from data walks.
- **Images**: Full interaction with the images at the bottom of the screen. Clicking on these images should expand into a scrolling list of images. Users should be able to overlay each image onto the current camera view to compare what the vacant lot was, and what it is today.
- **Community Contribution**: A plus button will be added to the bottom right of the screen. This will take users to a form where they can add their own anecdotes about each location. Additionally, they can add images.
- **Style**: The current, simple style of the pop-up was made for the demo. In the future, the design of the elements in the pop-up will be improved and tweaked.

### Map Screen
- **AOI Icons**: Icons on the map screen will be improved and changed based on user feedback. Feedback from the showcase did not provide any conclusive evidence on which styles of icons users like.
- **Map Styles**: Users will be able to select different map styles (sattelite, google maps style, etc) in a settings menu.
- **Guidance**: Users can be guided to a nearby AOI, likely using Mapbox or Niantic's routing capabilities.

### Showcase Feedback
> This feedback was collected from various users at the DNID Capstone Showcase on 12/9/25
- **Tutorial**: People that interacted with the demo were not sure what to do without me explaining it to them. In the future, there will be a tutorial screen that explains the purpose of the app as well as what to do at each AOI.
- **Non-vacant lot AOIs**: Some users indicated that they would like to interact with other areas on interest within Swissvale. Consider adding other AOIs such as memorial statues in the future.
- **Search**: Users indicated they would like to be able to search terms that relate to various AOIs. In the future, users could search terms and be guided to any AOIs that match with the search result.
- **Player Character Design**: Some users were not sure where they were on the map screen. Consider making the player character stick out more and change style/increase boldness on the direction indicator.
- **Timeline**: Users indicated that if they were going to look at a list of images, they would like them to be in chronological order. Consider adding a timeline of images within the image menu.

### Co-design Sessions

In order to improve the functionality and design of the project, co-design sessions will be conducted. These will interface directly with the community of Swissvale to ensure that the project connects with them.

> Improvements to the project were made over the course of the semester using feedback from in-class workshop sessions, feedback from a classmate who is a resident of Swissvale, feedback from project sponsors, and feedback from the end of semester showcase. Further gathering feedback from Swissvalians will allow the project to cater more towards them.

### Accessibility

The current demo is not as accessible to all as I would like it to be. In the future, the following improvments will be made:

- **Tech Access**: Not everyone has access to a mobile phone or a cellular connection. This project should be able to be fully interactable in the Carnegie Free Library of Swissvale *at minimum*.
- **Mobility**: In the future, users should be able to move around the player character without physically moving. This will allow those who aren't able to move around as easily in the physical world to interact with the project.


### Miscellaneous
- **Badges**: Implementing some sort of badge system to increase engagement. Users should get badges for completing various tasks, such as going to a certain number of AOIs.
- **Online Connectivity**: To increase connection within the community and promote collaboration, users should be able to connect with each other. This could be done as some sort of friend system.
- **Device Support**: Currently, the app only supports iPhones. In the future, this should be able to run on both iOS and Android devices. Additionally, users should be able to run this on desktops without using location data. Instead, they will be able to move the character around using keyboard controls or click on a AOI to investigate it.
- **Website**: A landing page will be made for the project to have somewhere to send users.
- **Library Engagement**: The Carnegie Free Library of Swissvale is a great place for the community to interact with this project. Consider collaborating with them on co-design/community engagement/promotion.