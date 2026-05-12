# Wanderer
# Introduction
# Credits
Time and Date: </br>
<a href = "https://time.now/developer"> World Time API </a> for consistent time and date fetching from anywhere in the world. 
[OpenWeatherAPI](https://openweathermap.org/api) for fetching weather data around the world.
# How to Use
# Programming Guidelines
Unity Version 2021.3.29f1 </br>
Unity AR Foundation </br>
Newtonsoft.JSON </br>
------------------------------------------------
Every fish species is created using a [scriptable object](https://docs.unity3d.com/6000.4/Documentation/Manual/class-ScriptableObject.html). These objects can be loaded into ```TemplateFishData.cs``` after setting the properties in the Object window.
<img width="556" height="459" alt="image" src="https://github.com/user-attachments/assets/afffbb18-f4bc-43a8-baa8-e0d442e37b57" /> </br>
Fish preferences will be fed into Template Fish Data, which then can double check with weather, time, human detection and audio managers.
# Customized Variations
I.E. How to add your own fish, change location of Wanderer, etc.

## Weather Location
In order to set the weather location, locate ```WeatherManager.cs``` 

You will need to change line #12:

```string url = "https://api.openweathermap.org/data/2.5/weather?q=Dallas&appid=0ffb08cb572db172c4a77e34ca5d5c25&units=imperial"; // Base URL```

to include your home city here: ```/weather?q=YOURCITYHERE&appid```

## Date/Time
### Using System Time vs Location Time
Time is handled in ```TimeAndDate.cs```. </br>
In the Sample Scene, there is a Time and Date script. In its inspector, you can type your City and Area into the appropriate strings in the inspector. </br>
**Example:**</br>
<img width="562" height="102" alt="image" src="https://github.com/user-attachments/assets/4388e7c1-96c8-4970-9ccb-c124bebcc0c0" /> </br>
If "Use System Time" is not checked, now Wanderer will utilize the time of the area you have specified. 
<img width="311" height="25" alt="image" src="https://github.com/user-attachments/assets/355e0871-23f3-489b-a2fb-c56e67883447" /> </br>

Find your region and city format here: <a href = "https://time.now/developer"> World Time API </a>


## Adding Species
### Creating a New Scriptable Object
You can create a new fish species by navigating to the project window and right clicking to open the menu. Then, go to ```Create>>>ScriptibleObjects>>>Fish``` and name the new Fish species appropriately. 

You can input species data such as: </br>
**FishName**: The name of the species.</br>
**MaxTemp**: The maximum temperature the Fish will tolerate. </br>
**MinTemp**: The minimum temperature the Fish will tolerate.</br>
**MaxDB**: The maximum decibel reading the fish will tolerate.</br>
**MinimumDB** The minimum decibel reading the fish will tolerate.</br>
**FaceDetectable** Whether or not the fish responds to human facial recognition.</br>

Each new fish will need to be constructed with the following: </br>
**PF_BasicFish**: The base prefab from which you will construct the fish. 
**TemplateFishData**: The script which contains the species data scriptable object created by the user.
**Your Fish Model**: The new fish model created by the user. This will be placed within the "Visuals" Game Object of PF_BasicFish.

### Importing Models and Textures
## Modifying Paths
Wanderer utilizes the A* Navigation Package. 
To edit species paths, locate the SwimSpots GameObject in the project Hierarchy. 
Swim spots are categorized between High, Medium and Low spots. You can duplicate these spots to create more points fish can travel between. All spots have their tag set to `Navigation`.
Then you will need to locate the ```A_Star``` GameObject. In the object's Inspector, locate the ```Pathfinder``` and click **Scan** to add these points to the graph. 
All fish have a "targets" list in their respective manager. You can fill this targets list with swim spots for the fish to travel between. 
## Facial Recognition 
Changing the 
 


