using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeAndDate : MonoBehaviour
{
    public TextMeshProUGUI timestring;
    public TextMeshProUGUI datestring;
    public string monthstring;
    public string monthstringText;

    [Header("TimeObjects")]
    public GameObject[] PostProcessingObject;
    public int timeTick;
    public int ThisMonth;
    public float timeRefresh;

    [Header("FishManager")]
    public bool YellowBassActive;
    public bool TurtleActive = true;
    public bool BGActive = true;
    public bool LNGarActive = true;
    public bool PaddleActive = true;


    public enum MonthofYear
    {
        None,
        Jan,
        Feb,
        Mar,
        Apr,
        May,
        Jun,
        Jul,
        Aug,
        Sept,
        Oct,
        Nov,
        Dec
    }

    public enum TimeofDay
    {
        None,
        Dawn,
        Day,
        Evening,
        Night
    }

    public TimeofDay sunPosition = TimeofDay.None;
    public MonthofYear monthYear = MonthofYear.None;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var currentDateTime = System.DateTime.UtcNow.ToLocalTime(); // Capture current time once per frame
        int currentMonth = System.DateTime.UtcNow.ToLocalTime().Month;
        monthstring = currentMonth.ToString();
        timeTick = System.DateTime.UtcNow.ToLocalTime().Hour; // Use the stored value for timeTick

        timestring.text = currentDateTime.ToString("HH:mm"); // Use the stored value for displaying time
        datestring.text = currentDateTime.ToString("yyyy-MM-dd"); // Use the stored value for displaying date

        Debug.Log("Current Local Time Hour: " + currentDateTime.Hour); // Use the same stored value for logging
        if(currentMonth == 1)
        {
            monthYear = MonthofYear.Jan;
          

        }
        else if(currentMonth == 2)
        {
            monthYear = MonthofYear.Feb;
            
        }
        else if (currentMonth == 3)
        {
            monthYear = MonthofYear.Mar;
        }
        else if (currentMonth == 4)
        {
            monthYear = MonthofYear.Apr;
        }
        else if (currentMonth == 5)
        {
            monthYear = MonthofYear.May;
        }
        else if (currentMonth == 6)
        {
            monthYear = MonthofYear.Jun;
        }
        else if (currentMonth == 7)
        {
            monthYear = MonthofYear.Jul;
        }
        else if (currentMonth == 8)
        {
            monthYear = MonthofYear.Aug;
        }
        else if (currentMonth == 9)
        {
            monthYear = MonthofYear.Sept;
        }
        else if (currentMonth == 10)
        {
            monthYear = MonthofYear.Oct;
        }
        else if (currentMonth == 11)
        {
            monthYear = MonthofYear.Nov;
        }
        else if (currentMonth == 12)
        {
            monthYear = MonthofYear.Dec;
           
        }
        //SUN POSITION
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

        //SET POST PROCESSING FOR SUN
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
        //SET FISH FOR MONTH OF YEAR.
        switch (monthYear)
        {
            case MonthofYear.None:
                break;

            case MonthofYear.Jan:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = false;
                PaddleActive = true;
                break;
            case MonthofYear.Feb:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = false;
                PaddleActive = true;

                break;
            case MonthofYear.Mar:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                break;
            case MonthofYear.Apr:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                break;
            case MonthofYear.May:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                break;
            case MonthofYear.Jun:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                break;
            case MonthofYear.Jul:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                break;
            case MonthofYear.Aug:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                break;
            case MonthofYear.Sept:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                break;
            case MonthofYear.Oct:
                YellowBassActive = true;
                BGActive = true;
                TurtleActive = true;
                LNGarActive = true;
                PaddleActive = true;
                break;
            case MonthofYear.Nov:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = false;
                PaddleActive = true;
                break;
            case MonthofYear.Dec:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = false;
                PaddleActive = true;
                break;

        }
        //SET APPEARANCE BASED ON TIME
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
        if(timeTick>3 && timeTick < 19)
        {
            PaddleActive = true;
        }
        else
        {
            PaddleActive = false;
        }

       ThisMonth = timeTick = System.DateTime.UtcNow.ToLocalTime().Month;
        print(ThisMonth);
    }
    public void CheckMonth()
    {
        if (ThisMonth > 2 && ThisMonth < 5)
        {
            PaddleActive = false;
        }
        else
        {
            PaddleActive = true;
        }
    }

}
