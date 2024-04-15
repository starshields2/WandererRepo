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

    [Header("FishManager")]
    public bool YellowBassActive = true;

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
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm");
        timestring.text = time;
        string date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        datestring.text = date;

        //if time is between x and y switch to other post processing object. 
        timeTick = System.DateTime.UtcNow.ToLocalTime().Hour;
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

        //Fish manager stuff
        if(timeTick > 11 && timeTick < 17)
        {
            YellowBassActive = false; 
        }
        else
        {
            YellowBassActive = true;
        }



    }
}
