# Exploring Augmented Reality Interface Designs for Virtual Meetings in Real-world Walking Contexts
Chiao-Ju Chang, Yu Lun Hsu, Wei Tian Mireille Tan, Yu-Cheng Chang, Pin Chun Lu, Yu Chen, Yi-Han Wang, and Mike Y. Chen.
DIS'24: ACM Conference on Designing Interactive Systems 2024

## Overview
We conducted this system to explore augmented reality (AR) interface design for virtual meetings in real-world walking conditions. We provide several hand gestures to design window properties, and two anchoring modes for window behaviors.

## System Functions
We provide following functions to design and use the user interface while walking:
+ **Gesture**: adjust window properties <img src="https://i.imgur.com/rf7pRHI.png" width="700">
    + **Pinch and drag** for **Position**: repositioning the window
    + **Size**: Pinch fingers with two hands and pull the hands apart to resize the window
    + **Opacity**: Increase or decrease the opacity of the window by extending the index finger and moving it up or down
    + **Rotation (Pitch)**:  Flex and extend the wrist while extending all four fingers to adjust the pitch rotation of the window
    + **Rotation (Yaw)**: Pronate and supinate the wrist while extending the index finger and thumb to adjust the yaw rotation of the window.
+ **Anchoring mode**: 
    + **Head-Anchored**: All windows appear in fixed positions relative to the user’s head orientation and rotate as the head rotates.
    + **Path-Anchored**: All windows appear in fixed positions relative to the forward walking path of the user, and do not rotate when the head rotates.
+ **Visibility Toggle**: using "*Thumbs-Up*" gesture can quickly change the visibility on/off when needed.
## Run by already build
We already build the APK for *Meta Oculus Quest Pro* in `Build/` folder. 
## System Set-Up (for developers)
The source code of the project is in `Unity Project/` folder. 
### Environment
+ Unity 2021.3.22f1
+ Unity Packages
    + Oculus XR Plugin 3.2.3
    + XR Plugin Management 4.3.3
    + 3D WebView for Android and iOS 4.2.2 (for running on headsets)
    + 3D WebView for Windows and macOS 4.2 (for computer developing)
+ Oculus Quest open **Passthrough** mode
