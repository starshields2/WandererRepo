using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;

public class EnvironmentManager : MonoBehaviour
{
    string apiKey = "0ffb08cb572db172c4a77e34ca5d5c25"; // Replace with your OpenWeatherMap API key
    string url = "https://api.openweathermap.org/data/2.5/weather?q=Dallas&appid=0ffb08cb572db172c4a77e34ca5d5c25&units=imperial"; // Base URL

    public GameObject debugui;
    [Header("Weather")]
    public float weatherUpInt = 300f;
    public float localTemp;
    public string weatherReport;
    public string rainReport;
    public string weatherDescriptionReport; // New field for weather description
    public TextMeshProUGUI tempToString;
    public TextMeshProUGUI rainToString;
    public TextMeshProUGUI weatherDescriptionToString; // New field for weather description
    public bool isRainy;
    public bool isDrought;
    public bool isSunny;

    [Header("WaterLevel")]
    public float waterLevel;

    [Header("Time")]
    public bool isDay;
    public bool isNight;
    [Header("WeatherObjects")]
    public GameObject rainFX;

    [SerializeField]
    string longitude, latitude;

    void Start()
    {
        
      
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location not enabled!");
        }
        else
        {
            Debug.Log("Gathering location");
            StartCoroutine(GetLocation());
        }
        StartCoroutine(GetWeatherNow());
    }

    IEnumerator GetLocation()
    {
        // Start location service
        Input.location.Start();

        // Wait for location service to initialize
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If location service didn't initialize in time or failed
        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Failed to get device location.");
            yield break;
        }
        else
        {
            // Get latitude and longitude
            latitude = Input.location.lastData.latitude.ToString();
            longitude = Input.location.lastData.longitude.ToString();

            Debug.Log("Latitude: " + latitude + ", Longitude: " + longitude);
        }

        // Stop location service
        Input.location.Stop();

        // Make weather update call
      //  OnlineWeatherUpdate();
    }

    void OnlineWeatherUpdate() => StartCoroutine(ShowandLoadWeatherData());

    IEnumerator ShowandLoadWeatherData()
    {
        // Create URL with latitude, longitude, and API key
        string requestUrl = url + "&lat=" + latitude + "&lon=" + longitude + "&units=imperial" + "&exclude=hourly,daily&appid=" + apiKey;

        UnityWebRequest www = UnityWebRequest.Get(requestUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching weather data: " + www.error);
        }
        else
        {
            // Parse JSON response
            string jsonResponse = www.downloadHandler.text;
            //Weather Data = OpenWeather API Response.
            WeatherData weatherData = JsonUtility.FromJson<WeatherData>(jsonResponse);

            // Access temperature, rain, and weather description from current weather data
            float temperature = weatherData.main.temp;
            var rain = weatherData.main.rain.oneHour;
            string description = weatherData.weather[0].description; // Assuming there's only one weather entry

            Debug.Log(jsonResponse);
            Debug.Log("Current temperature: " + temperature + "°F");
            localTemp = temperature;
            Debug.Log("Current rain: " + rain);
            Debug.Log("Weather description: " + description);

            // Display temperature, rain, and weather description
            weatherReport = "Temp: " + temperature;
            rainReport = "Rain: " + rain;
            weatherDescriptionReport = "Description: " + description;
            tempToString.text = weatherReport;
            rainToString.text = rainReport;
            weatherDescriptionToString.text = weatherDescriptionReport;
        }

        if (weatherDescriptionReport.Contains("rain") || (weatherDescriptionReport.Contains("thunderstorm")))
        {
            isRainy = true;
        }
        else
        {
            isRainy = false;
        }
    }

    void Update()
    {
        //OnlineWeatherUpdate();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Checking weather...");
            OnlineWeatherUpdate();
            debugui.SetActive(true);
        }

        if (Input.touchCount > 0)
        {
            // Loop through all the touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                // Check if the touch phase is began (finger touched the screen)
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    OnlineWeatherUpdate();

                    debugui.SetActive(true);

                }
            }
        }
        if (isRainy)
        {
            rainFX.SetActive(true);
        }
        else
        {
            rainFX.SetActive(false);
        }
    }

    public IEnumerator GetWeatherNow()
    {

        while (true) {
            OnlineWeatherUpdate();
            Debug.Log("updated weather.");
            yield return new WaitForSeconds(weatherUpInt);
        }
    }
}

[Serializable]
public class WeatherData
{
    public MainData main;
    public List<Weather> weather;
}

[Serializable]
public class MainData
{
    public float temp;
    public RainData rain; // Assuming rain data is provided in the JSON response
}

[Serializable]
public class RainData
{
    public float oneHour;
}

[Serializable]
public class Weather
{
    public string description;
}
