using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGillController : MonoBehaviour
{
    public GameObject[] allBGill;
    public FollowBeziCurve[] bGillScript;
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
        //for each BlueGill, set hiding time to two minutes.
        foreach (FollowBeziCurve fish in bGillScript)
        {
            fish._hideTime = 120f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //month & time
        if(timeManager.BGActive == false)
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
        if (weaManager.localTemp < 65f || weaManager.localTemp > 85f)
            {
            foreach (FollowBeziCurve fish in bGillScript)
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
        if (humanDetection.face == true) {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            foreach (FollowBeziCurve fish in bGillScript)
            {
                Debug.Log("FACE HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        //sound
       
        if(audioManager.textDb >= 85)
        {
            Debug.Log("should hide - from bgcontrol");
            foreach (FollowBeziCurve fish in bGillScript)
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
        foreach (GameObject fish in allBGill)
        {
            fish.SetActive(false);
        }
    }
    public void ActivateFish()
    {
        foreach (GameObject fish in allBGill)
        {
            fish.SetActive(true);
        }
    }
}
