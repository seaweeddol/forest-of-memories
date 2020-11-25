# Forest of Memories

An interactive journal experience created in Unity and utilizing IBM Watson’s Tone Analyzer API. Developed over the course of three weeks as my [capstone project](https://github.com/Ada-C13/capstone) during Ada Developer’s Academy.

### Video Demo

[![Forest of Memories screencap](http://img.youtube.com/vi/f00BKiYuwAI/0.jpg)](http://www.youtube.com/watch?v=f00BKiYuwAI "Forest of Memories")

# Installation

If you would like to play Forest of Memories, you can download it for Windows or Mac OS from [itch.io](https://seaweeddol.itch.io/forest-of-memories).

If you would like to see how it's built in Unity, you will need to install Unity and get an API key from IBM Cloud.

## Unity installation
1. If this is your first time using Unity, install [Unity Hub](https://unity3d.com/get-unity/download)
2. Clone this repository to your local machine
3. Open up Unity Hub.
4. From the Projects tab, select "Add"
5. Navigate to the location where you cloned the repository to
6. Select the folder and then select Open
7. The folder will be added to your Projects list in Unity Hub

## Getting an API Key
An API key is required in order to analyze player entries and create a tree.
1. Sign up for an [IBM Cloud account](https://www.ibm.com/cloud)
2. From your dashboard, under "Resources summary", select "Add resources"
3. In the "Search the catalog..." input field, type "tone analyzer"
4. Select the "Tone Analyzer" service
5. Scroll to the bottom of the screen, and type in a Service Name (this can be anything)
6. Click the Create button on the right hand panel to add the service
7. Go to the Manage tab
8. Click the copy icon to the right of the API key under Credentials

## Adding the API key to Unity
1. In Unity, find the "ScriptedObjects" game object within the Hierarchy window
2. Click the arrow to the left to expand this object
3. Select the "ToneAnalyzer" GameObject
4. In the Inspector tab, find the "Tone Analyzer (Script)" component 
5. Paste your API key into the "API_KEY" field
