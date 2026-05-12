using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class TimeAndDate : MonoBehaviour
{
    private string url = "https://time.now/developer/api/timezone/Africa/Ghana/Accra";
    public string MyCity;
    public string MyArea;
    public bool useSystemTime;

    public Transform _daylight;
    public TextMeshProUGUI timestring;
    public TextMeshProUGUI datestring;
    public string monthstring;
    public string monthstringText;
    int testingHours;
    DateTimeOffset dt;

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
    public bool crawActive = true;
    public int currentMonth;

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

    public class DateTimeJSONData
    {
        public string abbreviation { get; set; }
        public string datetime;
        public int day_of_week { get; set; }
        public int day_of_year { get; set; }
        public bool dst { get; set; }
        public int dst_offset { get; set; }
        public string timezone { get; set; }
        public int unixtime { get; set; }
        public DateTime utc_datetime { get; set; }
        public string utc_offset { get; set; }
        public int week_number { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    [ContextMenu("GetDataRequest")]
    public void GetDataRequest()
    {
        StartCoroutine(GetRequest("https://time.now/developer/api/timezone" + "/" + MyArea + "/" + MyCity));
    }
    public IEnumerator GetRequest(string uri)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
      
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong: {0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    DateTimeJSONData data = JsonConvert.DeserializeObject<DateTimeJSONData>(webRequest.downloadHandler.text);
                    //Debug.Log("DATA REQUEST:" + data.datetime);
                    dt = DateTimeOffset.Parse(data.datetime);
                    Debug.Log(dt.ToString());
                    testingHours = dt.TimeOfDay.Hours;
                   // Debug.Log("THE HOUR IN " + MyCity + " IS: " + testingHours);
                    //Debug.Log(webRequest.downloadHandler.text);

                    break;
            }
        }
        }

    

    // Update is called once per frame
    void Update()
    {
        if (useSystemTime)
        {
            var currentDateTime = System.DateTime.UtcNow.ToLocalTime();
            timeTick = System.DateTime.UtcNow.ToLocalTime().Hour;
            timestring.text = currentDateTime.ToString("HH:mm"); // Use the stored value for displaying time
            datestring.text = currentDateTime.ToString("yyyy-MM-dd"); // Use the stored value for displaying date
            currentMonth = System.DateTime.UtcNow.ToLocalTime().Month;
            monthstring = currentMonth.ToString();
        }
        else if (!useSystemTime)
        {
            GetDataRequest();
            timeTick = testingHours;
           
            timestring.text = dt.DateTime.ToString("HH:mm");
            datestring.text = dt.DateTime.ToString("yyyy-MM-dd");
        }

         // Capture current time once per frame
       
        // Use the stored value for timeTick

        

       // Debug.Log("Current Local Time Hour: " + currentDateTime.Hour); // Use the same stored value for logging
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
                crawActive = false;
                break;
            case MonthofYear.Feb:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = false;
                PaddleActive = true;
                crawActive = false;

                break;
            case MonthofYear.Mar:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                crawActive = true;

                break;
            case MonthofYear.Apr:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                crawActive = true;
                break;
            case MonthofYear.May:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                crawActive = true;
                break;
            case MonthofYear.Jun:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                crawActive = true;
                break;
            case MonthofYear.Jul:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                crawActive = true;
                break;
            case MonthofYear.Aug:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                crawActive = true;
                break;
            case MonthofYear.Sept:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = true;
                PaddleActive = true;
                crawActive = true;
                break;
            case MonthofYear.Oct:
                YellowBassActive = true;
                BGActive = true;
                TurtleActive = true;
                LNGarActive = true;
                PaddleActive = true;
                crawActive = true;
                break;
            case MonthofYear.Nov:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = false;
                PaddleActive = true;
                crawActive = false;
                break;
            case MonthofYear.Dec:
                YellowBassActive = true;
                BGActive = true;
                LNGarActive = true;
                TurtleActive = false;
                PaddleActive = true;
                crawActive = false;
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
        if (timeTick > 18 || timeTick < 5)
        {
            crawActive = false;
        }
        else
        {
            crawActive = true;
        }
        if (timeTick >= 18 || timeTick <= 11)
        {
            LNGarActive = true;
        }
        else
        {
            LNGarActive = false;
        }


        ThisMonth = timeTick = System.DateTime.UtcNow.ToLocalTime().Month;
        //print(ThisMonth);
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
