using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeAndDate : MonoBehaviour
{
    public TextMeshProUGUI timestring;
    public TextMeshProUGUI datestring;

    [Header("TimeObjects")]
    public GameObject[] PostProcessingObject;
    public int timeTick;
    public int ThisMonth;

    [Header("FishManager")]
    public bool YellowBassActive = true;
    public bool TurtleActive = true;
    public bool BGActive = true;
    public bool LNGarActive = true;

    public enum TimeofDay
    {
        None,
        Dawn,
        Day,
        Evening,
        Night
    }

    public TimeofDay sunPosition = TimeofDay.None;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var currentDateTime = System.DateTime.UtcNow.ToLocalTime(); // Capture current time once per frame
        timeTick = currentDateTime.Hour; // Use the stored value for timeTick

        timestring.text = currentDateTime.ToString("HH:mm"); // Use the stored value for displaying time
        datestring.text = currentDateTime.ToString("yyyy-MM-dd"); // Use the stored value for displaying date

        Debug.Log("Current Local Time Hour: " + currentDateTime.Hour); // Use the same stored value for logging

        if (timeTick >= 4 && timeTick < 6)
        {
            sunPosition = TimeofDay.Dawn;
        }
        else if (timeTick >= 6 && timeTick < 17)
        {
            sunPosition = TimeofDay.Day;
        }
        else if (timeTick >= 17 && timeTick < 19)
        {
            sunPosition = TimeofDay.Evening;
        }
        else if (timeTick >= 19 && timeTick < 20)
        {
            sunPosition = TimeofDay.Night; // Assuming 19 to 20 is twilight, adjust as needed
        }
        else if (timeTick >= 20 && timeTick < 24)
        {
            sunPosition = TimeofDay.Night;
        }
        else if (timeTick >= 0 && timeTick < 4)
        {
            sunPosition = TimeofDay.Night;
        }
        switch (sunPosition)
        {
            case TimeofDay.None:
                break;
            case TimeofDay.Dawn:
                foreach (GameObject postP in PostProcessingObject)
                {
                    postP.SetActive(false);
                }
                PostProcessingObject[0].SetActive(true);
                break;
            case TimeofDay.Day:
                foreach (GameObject postP in PostProcessingObject)
                {
                    postP.SetActive(false);
                }
                PostProcessingObject[1].SetActive(true);
                break;
            case TimeofDay.Evening:
                foreach (GameObject postP in PostProcessingObject)
                {
                    postP.SetActive(false);
                }
                PostProcessingObject[2].SetActive(true);
                break;
            case TimeofDay.Night:
                foreach (GameObject postP in PostProcessingObject)
                {
                    postP.SetActive(false);
                }
                PostProcessingObject[3].SetActive(true);
                break;
        }

        //Fish manager stuff. FIND A MORE EFFICIENT WAY
        if(timeTick > 11 && timeTick < 17)
        {
            YellowBassActive = false;
            LNGarActive = false;
        }
        else
        {
            YellowBassActive = true;
        }
        if (timeTick > 17 || timeTick < 6)
        {
            TurtleActive = false;
        }
        else
        {
            TurtleActive = true;
        }
        if (timeTick > 5 && timeTick < 22)
        {
            BGActive = true;
        }
        else
        {
            BGActive = false;
        }

       ThisMonth = timeTick = System.DateTime.UtcNow.ToLocalTime().Month;
        print(ThisMonth);
    }
    public void CheckMonth()
    {
        if (ThisMonth > 2 && ThisMonth < 6)
        {
            YellowBassActive = false;
        }
    }
}
