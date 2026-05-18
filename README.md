# 1. Wanderer
Wanderer is a speculative posthuman-computer interaction (postHCI) project exploring multispecies coexistence through environmental simulation, ecological sensing, and un-interactive interaction (Lee & Obuobi, 2026). 
- Lee, Y.-J. & Obuobi, S. (2026). Cohabiting with Semi-virtual Nature via Wanderer: Speculative Design in Taoist Post-HCI. World Futures Review, 0(0). https://doi.org/10.1177/19467567261438429 </br>

Wanderer © 2026 by Yueh-Jung Lee, Alrisha James Obuobi is licensed under CC BY-NC 4.0. To view a copy of this license, visit https://creativecommons.org/licenses/by-nc/4.0/ 

# 1.1. Overview 
Wanderer is an open-source interactive application that simulates local aquatic ecosystems through real-time environmental data, ambient sensing, and species-specific behaviors. 

Unlike conventional interactive applications designed primarily for human engagement, Wanderer explores a different relationship between humans, technology, and nonhuman life. The project draws from: 
- posthumanism,
- Taoist philosophy,
- speculative design,
- environmental psychology,
- and critical human-computer interaction (HCI). </br>

In Wanderer, virtual aquatic species do not exist to entertain, obey, or maximize user engagement. Their appearance and behavior depend on ecological and ambient conditions shared between human and nonhuman actors. The system responds to: 
- weather,
- temperature,
- sound levels,
- time of day,
- movement,
- and environmental rhythms. </br>

Interaction occurs indirectly, environmentally, and reciprocally. 

## 1.2. Core Philosophy 
Wanderer challenges anthropocentric assumptions embedded in conventional interactive systems. Rather than optimizing for: 
- engagement,
- retention,
- gamification,
- or behavioral extraction, </br>

Wanderer explores: 
- coexistence,
- restraint,
- ecological awareness,
- and multispecies reciprocity. </br>

The project is heavily influenced by: 
- Taoist concepts of Tao and wu wei,
- feminist and critical posthuman theory,
- speculative design,
- and environmental humanities. </br>

The interaction model of Wanderer is intentionally minimal. Human actors cannot directly command or control the species. Instead, species appear only when environmental and ambient conditions become mutually habitable. 

# 2. Design Principles 
1. Prioritize Environmental Conditions. Environmental factors are treated as primary interactors rather than passive backgrounds. The system responds to weather, time, sound, temperature, and ecological rhythms.
2. Refuse Anthropocentric Interaction. The virtual species are not designed as pets, companions, collectibles, or game objects. They are treated as autonomous environmental actors with their own preferences, rhythms, tolerances, and social behaviors.
3. Minimize Human Manipulation. Wanderer intentionally reduces direct control. There are no tapping mechanics, no feeding systems, no achievement loops, no progression systems, no monetized engagement structures. Interaction happens indirectly through environmental coexistence.
4. Practice Wu Wei (Un-Interactive Interaction). The system draws from the Taoist concept of wu wei — action through non-forcing. Species emerge when humans remain calm, ambient conditions stabilize, and the environment becomes hospitable. The experience rewards attentiveness rather than control.
5. Minimize Resource Use. Wanderer emphasizes lightweight and sustainable computing practices: low-poly models, minimal data retention, reduced processing load, low-frequency API polling, compatibility across devices. 

# 3. Features 
1. Real-time weather synchronization
2. Dynamic time-of-day simulation
3. Ambient sound responsiveness
4. Human presence sensing
5. Species-specific ecological behavior
6. Local ecosystem simulation
7. Underwater environmental audio
8. Ecological/sensory/social/data flow systems
9. Android tablet deployment
10. Low-resource rendering pipeline 

# 4. Interaction Model 
Wanderer operates through four interconnected flows. 

## 4.1. Flow System 
### 4.1.1. Ecological Flow 
Environmental conditions: 
- weather,
- season,
- time of day,
- temperature. 

### 4.1.2. Sensory Flow 
Species comfort conditions: 
- sound levels,
- temperature ranges,
- movement sensitivity,
- preferred habitats. 

### 4.1.3. Social Flow 
Relationships between species: 
- attraction,
- avoidance,
- territorial behavior,
- coexistence. 

### 4.1.4. Data Flow 
Real-time system processing: 
- environmental APIs,
- sensor data,
- audio detection,
- local computation. </br>

No behavioral data is persistently stored. 

## 4.2. Example Interaction 
A Yellow Bass may only appear: 
- during nighttime hours,
- within a preferred temperature range,
- under low-noise conditions,
- and when human movement remains minimal. </br>

If the environment becomes loud or disruptive, the species may: 
- hide,
- swim away,
- or disappear entirely. 

# 5 - Technical Stack
## 5.1. Current Platform 
- Android
- Unity (Version: 2021.3.29f1) 

## 5.2. Device Sensors 
- Front camera
- Microphone
- Internet/weather APIs 
- System clock (optional)

## 5.3. APIs 
- [World Time API](https://time.now/developer) for consistent time and date fetching from anywhere in the world.
- [OpenWeather API](https://openweathermap.org/api) for fetching weather data around the world. 

## 5.4. Audio 
- Hydrophone recordings from the Trinity River 

# 6. Repository Structure 

This repo contains the base project, which includes 5 fish species and the basic framework under which they operate.  

<img width="1150" height="648" alt="image" src="https://github.com/user-attachments/assets/4ef8455f-2464-4a9b-9fce-ec7282a0d4ff" />



Above are images depicting the project in action. Fish will respond to:  

    month and year 

    time of day 

    weather conditions 

    surrounding audio 

    human presence (AR detected faces) 
# 7. Installation  

7.1. Requirements 

    Unity 2022+ 

    Android SDK 

    OpenWeather API Key 

    [date/time API] 

    Android tablet or emulator 

7.2. Setup 

    Clone Repository 

    git clone https://github.com/YOUR_USERNAME/wanderer.git 

    Open in Unity. Open the project using: 

    Unity Hub 

    Unity 2022+ 

    Configure Weather API 

    Create: 

    [code example] 

    Example: 

    [code example] 

    Configure [date/time] API 

    Create:  

    [code example]  

    Example:  

    [code example] 

    Enable Permissions  

    Allow: camera access, microphone access, internet access. 

    Build for Android  

    Use: File → Build Settings → Android → Build. Ensure that your Unity version has the Android Build Tools installed.
# 8. Creating a Regional Variant

One of Wanderer’s primary goals is enabling local ecological reinterpretation. Contributors are encouraged to create localized ecosystem variants. 

8.1. Critical Notes 

This section lists the elements that are opened to customization and  

8.1.1. What Can Be Localized 

    geological area
    
    weather conditions

    relevant visual assets 

    river/lake/ocean environment design 

    local weather-related scenes 

    aquatic species 

    species' biological data 

    environmental sounds 

    other cultural framing 

8.1.2. What Should Remain Consistent 

The project’s philosophical foundations should remain intact: 

    non-anthropocentric interaction 

    ecological reciprocity 

    un-interactive interaction 

    privacy-first sensing 

    anti-extractive data practices 

    non-gamified experience 

8.1.3. What Wanderer Is Not 

Wanderer is not: 

- a pet simulator, 

- a gamified productivity app, 

- a dopamine-maximizing engagement system, 

- a surveillance platform, 

- an extractive behavioral analytics tool. 
# 8.2 Programming Guidelines
## Packages and Versions:
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

