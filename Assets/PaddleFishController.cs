using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleFishController : MonoBehaviour
{
    public GameObject[] allPaddles;
    public FollowBeziCurve[] _paddleScript;
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
        foreach (FollowBeziCurve fish in _paddleScript)
        {
            fish._hideTime = 600f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //month & time
        if (timeManager.PaddleActive == false)
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
        if (weaManager.localTemp < 54f || weaManager.localTemp > 75f)
        {
            foreach (FollowBeziCurve fish in _paddleScript)
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
            foreach (FollowBeziCurve fish in _paddleScript)
            {
                Debug.Log("FACE HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        //sound

        if (audioManager.textDb >= 65)
        {
            Debug.Log("should hide - from bgcontrol");
            foreach (FollowBeziCurve fish in _paddleScript)
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
        foreach (GameObject fish in allPaddles)
        {
            fish.SetActive(false);
        }
    }
    public void ActivateFish()
    {
        foreach (GameObject fish in allPaddles) { 
            fish.SetActive(true);
        }
    }
}
