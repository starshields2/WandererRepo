using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGill : MonoBehaviour
{
    [Header("Movement Controller")]
    public FollowBeziCurve beziMover;
    public TimeAndDate timemanager;
    [Header("Weather Controller")]
    public EnvironmentManager enviManager;

    [Header("Social Interaction")]
    public GameObject[] friendWith;
    public GameObject[] avoiding;
    public GameObject[] chasing;
    // Start is called before the first frame update
    void Start()
    {
        //friendWith[] = GameObject.FindGameObjectsWithTag("Yellow Bass");
    }

    // Update is called once per frame
    void Update()
    {
        if (timemanager.BGActive)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        if (enviManager.localTemp < 65f || enviManager.localTemp > 85f)
        {
            beziMover.currentState = FollowBeziCurve.FishState.Hide;
        }
        else
        {
            // 
        }

    }
}
