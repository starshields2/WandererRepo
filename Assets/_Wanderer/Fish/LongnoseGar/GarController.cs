using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarController : MonoBehaviour
{
    public GameObject[] allGar;
    public FollowBeziCurve[] _garScript;
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
        foreach (FollowBeziCurve fish in _garScript)
        {
            fish._hideTime = 300f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //month & time
        if (timeManager.LNGarActive == false)
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
        if (weaManager.localTemp < 60f || weaManager.localTemp > 90f)
        {
            foreach (FollowBeziCurve fish in _garScript)
            {
                tempOn = false;
                Debug.Log("TEMP HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        else
        {
            tempOn = true;
            foreach (FollowBeziCurve fish in _garScript)
            {
                tempOn = false;
                fish.currentState = FollowBeziCurve.FishState.RegularSwim;
            }
        }
        //face
        if (humanDetection.face == true)
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            foreach (FollowBeziCurve fish in _garScript)
            {
                Debug.Log("FACE HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        else
        {
            foreach (FollowBeziCurve fish in _garScript)
            {
                tempOn = false;
                fish.currentState = FollowBeziCurve.FishState.RegularSwim;
            }
        }
        //sound

        if (audioManager.textDb >= 75)
        {
            Debug.Log("should hide - from bgcontrol");
            foreach (FollowBeziCurve fish in _garScript)
            {
                soundOn = false;
                Debug.Log("SOUND HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        else
        {
            soundOn = true;
            foreach (FollowBeziCurve fish in _garScript)
            {
                tempOn = false;
                fish.currentState = FollowBeziCurve.FishState.RegularSwim;
            }
        }
    }

    [ContextMenu("DeactivateFish")]


    public void DeactivateFish()
    {
        foreach (GameObject fish in allGar)
        {
            fish.SetActive(false);
        }
    }
    public void ActivateFish()
    {
        foreach (GameObject fish in allGar)
        {
            fish.SetActive(true);
        }
    }
}
