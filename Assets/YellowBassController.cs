using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBassController : MonoBehaviour
{
    public GameObject[] allYBass;
    public FollowBeziCurve[] yBassScript;
    public HumanDetection humanDetection;
    public TimeAndDate timeManager;
    public MicrophoneManager audioManager;
    public WeatherManager weaManager;

    public float tempCheck;

    public bool tempOn;
    public bool soundOn;
    public bool faceOn;
    public bool timeOn;

    // Start is called before the first frame update
    void Start()
    {
        tempCheck = weaManager.localTemp;
        foreach (FollowBeziCurve fish in yBassScript)
        {
            fish._hideTime = 120f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //month & time
        if (timeManager.YellowBassActive == false)
        {
            Debug.Log("No Fish Active");
            DeactivateFish();
            timeOn = false;
        }
        else
        {
            timeOn = true;
            Debug.Log("Fish Active");
            ActivateFish();
        }
        //temp 
        if (weaManager.localTemp < 62f || weaManager.localTemp > 82f)
        {
            foreach (FollowBeziCurve fish in yBassScript)
            {
                tempOn = false;
                Debug.Log("TEMP HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        else
        {
            tempOn = true;
        }
        //face
        if (humanDetection.face == true)
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            foreach (FollowBeziCurve fish in yBassScript)
            {
                Debug.Log("FACE HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        //sound

        if (audioManager.textDb >= 65)
        {
            Debug.Log("should hide - from bgcontrol");
            foreach (FollowBeziCurve fish in yBassScript)
            {
                soundOn = false;
                Debug.Log("SOUND HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        else
        {
            soundOn = true;
        }
    }

    [ContextMenu("DeactivateFish")]


    public void DeactivateFish()
    {
        foreach (GameObject fish in allYBass)
        {
            fish.SetActive(false);
        }
    }
    public void ActivateFish()
    {
        foreach (GameObject fish in allYBass)
        {
            fish.SetActive(true);
        }
    }
}
