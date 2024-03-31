using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnvironmentManager : MonoBehaviour
{
    string apiKey = "0ffb08cb572db172c4a77e34ca5d5c25"; // Replace with your OpenWeatherMap API key
    string url = "https://api.openweathermap.org/data/2.5/weather?q=Dallas&appid=0ffb08cb572db172c4a77e34ca5d5c25&units=metric"; // Base URL

    [Header("Weather")]
    public bool isRainy;
    public bool isDrought;
    public bool isSunny;

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
        string requestUrl = url + "?lat=" + latitude + "&lon=" + longitude + "&exclude=hourly,daily&appid=" + apiKey;

        UnityWebRequest www = UnityWebRequest.Get(requestUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Checking weather...");
            OnlineWeatherUpdate();
        }
    }
}
