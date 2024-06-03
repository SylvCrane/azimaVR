# azimaVR

The Azima Virtual Reality (VR) Application acts as a companion application to the web-based platform of the same name. On the web-based application, a user can upload 360° images and create virtual tours easily.  
The VR Application interacts with the backend used for the web-based platform to view the created tours in full Virtual Reality. The VR Application, now, is not designed to create virtual tours. However, this is up to the future developer’s discretion. 
The VR Application allows a user to log in and view their private tours created in the web-based platform as well as view the publicly available tours. The user can then view any tour created in full Virtual Reality.  
The VR Application is primarily intended for use with the Meta Quest headsets, specifically the Meta Quest 2. However, the application can be easily configured to allow development on the current headset.  


# Prerequisites

Before beginning, the developer must have the following items installed on their machine: 
- Unity Hub: This allows the developer to open Unity projects
  - By extension, the developer requires a Unity account 
- Unity Version 2022.3.24f1.  
- Git: Required for pulling the repository 
  - By extension, the developer requires a GitHub account 
- GIT LFS (Large File System): Used to process files larger than 100MB for Git pushes. 
- IDE: The recommended IDE is Visual Studio 2019. This can be installed with Unity. 
- Oculus Desktop App: Used for communicating with the Meta Headset for debugging purposes

Furthermore, there are some Unity assets that the developer must download and install. They are) 
- Meta XR Core SDK https://assetstore.unity.com/packages/tools/integration/meta-xr-core-sdk-269169
- Meta XR Interaction SDK https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-ovr-samples-268521 
- GitHub Desktop: Allows for easy local and online Git manipulation, including cloning repositories and managing branches. 

# Installation

•	To install the source code of the project, you can clone the repository through GitHub desktop 
•	Please ensure you are on the main branch when cloning 
•	Open Unity Hub and add the project 
•	When the developer opens the project for the first time, there will be errors declared asking them to install. To install these, go to Edit->Project Settings->MetaXR and apply all of the changes. 

# Running the Application

•	Before running the application, make sure to connect your Meta Quest Headset to your PC via USB-C cable.  
•	On your Meta Quest Headset, select ‘Quest Link’ in the menu connect. You will then open the Quest Link menu, which allows you to run the Unity app off of your computer, but can be seen through the Meta Headset 
•	Use the ‘Play’ button to play the application at this point. 

# Deploying the Application

-	For deploying, make sure that you have allowed for development and debugging on  your Headset (A popup should appear when you connect it to your PC on the headset). Then, DO NOT enter Quest Link.  

-	In Unity, go to File->Build Settings. Here, you can configure the settings when building the application as an APK. Note that the build is in Android and is outputting to the Oculus Quest. If you performed the previous steps correctly, this should be the case.

# Authors

This project was developed by the following developers:

- Navjot Sandhu

# References
The following downloaded assets were used at some point during the development of 
the app. They are as follows)

Meta XR All-In-One-SDK by Oculus 
https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657
Located in Packages

Meta XR Interaction SDK by Oculus
https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-264559
Located in Packages

Wooden Floor Materials by Casual2D
https://assetstore.unity.com/packages/2d/textures-materials/wood/wooden-floor-materials-150564
Located in Resources->DownloadedAssets->Free

Low Poly Simple Furniture FREE by Gobormu
https://assetstore.unity.com/packages/3d/props/furniture/low-poly-simple-furniture-free-240197
Located in Resources->DownloadedAssets->Low Poly Furniture

House Props - Low Poly by Zero Grid
https://assetstore.unity.com/packages/3d/props/house-props-low-poly-266235
Located in Resources->DownloadedAssets->ZeroGrid

Ground Materials Sample - Photoscanned by Mikołaj Spychał
https://assetstore.unity.com/packages/2d/textures-materials/floors/grounds-materials-sample-photoscanned-55437
Located in Resources->DownloadedAssets->Photoscanned Grounds Materials Sample

SimpleJSON by bunny83
https://github.com/Bunny83/SimpleJSON/blob/master/SimpleJSONUnity.cs
Located in Plugins
