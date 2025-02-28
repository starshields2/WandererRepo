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
    public EnvironmentManager envManager;

    public float tempCheck;

    public bool tempOn;
    public bool soundOn;
    public bool faceOn;
    public bool timeOn;

    // Start is called before the first frame update
    void Start()
    {
        tempCheck = envManager.localTemp;
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
        if (envManager.localTemp < 60f || envManager.localTemp > 90f)
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
