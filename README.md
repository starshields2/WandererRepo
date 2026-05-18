# Wanderer

# Introduction
# Credits
Time and Date: </br>
<a href = "https://time.now/developer"> World Time API </a> for consistent time and date fetching from anywhere in the world. 
[OpenWeatherAPI](https://openweathermap.org/api) for fetching weather data around the world.
# 4 - Interaction Model 

Wanderer operates through four interconnected flows. 

4.1. Flow System 

4.1.1. Ecological Flow 

Environmental conditions: 

    weather, 

    season, 

    time of day, 

    temperature. 

4.1.2. Sensory Flow 

Species comfort conditions: 

    sound levels, 

    temperature ranges, 

    movement sensitivity, 

    preferred habitats. 

4.1.3. Social Flow 

Relationships between species: 

    attraction, 

    avoidance, 

    territorial behavior, 

    coexistence. 

4.1.4. Data Flow 

Real-time system processing: 

    environmental APIs, 

    sensor data, 

    audio detection, 

    local computation. 

No behavioral data is persistently stored. 

4.2. Example Interaction 

A Yellow Bass may only appear: 

    during nighttime hours, 

    within a preferred temperature range, 

    under low-noise conditions, 

    and when human movement remains minimal. 

If the environment becomes loud or disruptive, the species may: 

    hide, 

    swim away, 

    or disappear entirely. 
# 5 - Technical Stack
5.1. Current Platform 

    Android 

    Unity (Version: 2021.3.29f1) 

5.2. Device Sensors 

    Front camera 

    Microphone 

    System clock (optional) 

    Internet/weather APIs 

5.3. APIs 

    OpenWeather API 

    date/time API 

5.4. Audio 

    Hydrophone recordings from the Trinity River 

# 6 -Repository Structure 

This repo contains the base project, which includes 5 fish species and the basic framework under which they operate.  

<img width="1150" height="648" alt="image" src="https://github.com/user-attachments/assets/4ef8455f-2464-4a9b-9fce-ec7282a0d4ff" />



Above are images depicting the project in action. Fish will respond to:  

    month and year 

    time of day 

    weather conditions 

    surrounding audio 

    human presence (AR detected faces) 
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
In order to set the weather location, locate ```Weather Manager``` in the Hierarchy of your scene. Navigate to the WeatherManager.cs file in its Inspector panel. 
You can modify the weather by changing the "MyCity" string. 



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
You can create a new fish species by navigating to the project window and right clicking to open the menu. Then, go to ```Create>>>ScriptibleObjects>>>Fish``` and name the new Fish species appropriately.  You can look at the "Example Fish" Scriptable Object for a visual. </br>
<img width="417" height="586" alt="image" src="https://github.com/user-attachments/assets/07e55d39-17f4-4607-91c7-08bf75e80842" /></br>
You can input species data such as: </br>
**FishName**: The name of the species.</br>
**MaxTemp**: The maximum temperature the Fish will tolerate. </br>
**MinTemp**: The minimum temperature the Fish will tolerate.</br>
**MaxDB**: The maximum decibel reading the fish will tolerate.</br>
**MinimumDB** The minimum decibel reading the fish will tolerate.</br>
**Hide From Face** Whether or not the fish responds to human facial recognition.</br>
**Does Sunbathe** Whether or not the fish sunbathes.

Along with the starting and ending hour and month (0-2400, and 1-12 respectively).

Each new fish will need to be constructed with the following: </br>
**PF_BasicFish**: The base prefab from which you will construct the fish. 
**TemplateFishData**: The script which contains the species data scriptable object created by the user.
**Your Fish Model**: The new fish model created by the user. This will be placed within the "Visuals" Game Object of PF_BasicFish.

### Importing Models and Textures
Create your fish  model using any 3D package of your choice along with its textures and materials. Compile in Unity and place the new model under the "Visuals" GameObject in the template Fish. Place the "Visuals" object in the Visual Container aspect of the template fish data component. 

## Modifying Paths
Wanderer utilizes the A* Navigation Package. 
To edit species paths, locate the SwimSpots GameObject in the project Hierarchy. 
Swim spots are categorized between High, Medium and Low spots. You can duplicate these spots to create more points fish can travel between. All spots have their tag set to `Navigation`.
Then you will need to locate the ```A_Star``` GameObject. In the object's Inspector, locate the ```Pathfinder``` and click **Scan** to add these points to the graph. 
All fish have a "targets" list in their respective manager. This is filled by the "Swim Heights" conditions in each fish's Scriptable Objects container.
## Facial Recognition 
Each fish scriptable object should have a "hide from face" boolean. When checked this will feed into the "Camera Shy" attribute of the ```TemplateFishData.cs``` file. 

