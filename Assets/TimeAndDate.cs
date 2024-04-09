using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeAndDate : MonoBehaviour
{
    public TextMeshProUGUI timestring;
    public TextMeshProUGUI datestring;

    [Header("TimeObjects")]
    public GameObject daytime;

    private enum TimeofDay
    {
        Dawn,
        Day,
        Evening,
        Night
    }
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

    }

    
}
