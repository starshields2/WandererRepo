# Wanderer
# Introduction
# How to Use
# Programming Guidelines
# Customized Variations
## Weather Location
In order to set the weather location, locate ```WeatherManager.cs``` 

You will need to change line #12:

```string url = "https://api.openweathermap.org/data/2.5/weather?q=Dallas&appid=0ffb08cb572db172c4a77e34ca5d5c25&units=imperial"; // Base URL```

to include your home city here: ```/weather?q=YOURCITYHERE&appid```

## Date/Time
### Using System Time vs Location Time
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
 


