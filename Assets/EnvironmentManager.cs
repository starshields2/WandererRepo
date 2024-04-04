using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class EnvironmentManager : MonoBehaviour
{
    string apiKey = "0ffb08cb572db172c4a77e34ca5d5c25"; // Replace with your OpenWeatherMap API key
    string url = "https://api.openweathermap.org/data/2.5/weather?q=Toronto&appid=0ffb08cb572db172c4a77e34ca5d5c25&units=imperial"; // Base URL

    [Header("Weather")]
    public string weatherReport;
    public string rainReport;
    public TextMeshProUGUI tempToString;
    public TextMeshProUGUI rainToString;
    public bool isRainy;
    public bool isDrought;
    public bool isSunny;
    public GameObject rainWeather;

    [Header("WaterLevel")]
    public float waterLevel;

    [Header("Time")]
    public bool isDay;
    public bool isNight;

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
        OnlineWeatherUpdate();
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

            // Access temperature and rain from current weather data
            float temperature = weatherData.main.temp;
            float rain = weatherData.main.rain.oneHour;

            Debug.Log(jsonResponse);
            Debug.Log("Current temperature: " + temperature + "°F");
            Debug.Log("Current rain: " + rain);

            // Display temperature
            weatherReport = "Temp: " + temperature;
            rainReport = "Rain: " + rain;
            tempToString.text = weatherReport;
            rainToString.text = rainReport;

            //update bools
            if(rain > 0)
            {
                isRainy = true;
                
            }
            else
            {
                isRainy = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Checking weather...");
            OnlineWeatherUpdate();
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
                }
            }
        }
        if (isRainy)
        {
            rainWeather.SetActive(true);
        }

        if (!isRainy)
        {
            rainWeather.SetActive(false);
        }
    }

}


[Serializable]
public class WeatherData
{
    public MainData main;
}

[Serializable]
public class MainData
{
    public float temp;
    public Minutely precipitation;
    public RainData rain; // Assuming rain data is provided in the JSON response
}

[Serializable]
public class RainData
{
    public float oneHour;
}
[Serializable]
public class Minutely
{
    public float precipitation;
}
