using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    public GameObject[] allTurt;
    public FollowBeziCurve[] turtleScript;
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
        foreach (FollowBeziCurve fish in turtleScript)
        {
            fish._hideTime = 600f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //month & time
        if (timeManager.TurtleActive == false)
        {
            Debug.Log("No Turtle Active");
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
        if (weaManager.localTemp < 75f || weaManager.localTemp > 85f)
        {
            foreach (FollowBeziCurve fish in turtleScript)
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
            foreach (FollowBeziCurve fish in turtleScript)
            {
                Debug.Log("FACE HIDE");
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        //sound

        if (audioManager.textDb >= 75)
        {
        
            foreach (FollowBeziCurve fish in turtleScript)
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

        //SUNNY

        if(weaManager.isSunny = true)
        {
            foreach (FollowBeziCurve fish in turtleScript)
            {
                soundOn = false;
                Debug.Log("SOUND HIDE");
                fish.currentState = FollowBeziCurve.FishState.Sunbathe;
            }
        }
    }

    [ContextMenu("DeactivateFish")]


    public void DeactivateFish()
    {
        foreach (GameObject fish in allTurt)
        {
            fish.SetActive(false);
        }
    }
    public void ActivateFish()
    {
        foreach (GameObject fish in allTurt)
        {
            fish.SetActive(true);
        }
    }
}
