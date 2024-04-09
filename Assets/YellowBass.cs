using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using TasiYokan.Curve;

public class YellowBass : MonoBehaviour
{
    [Header("Movement Controller")]
    public FollowBeziCurve beziMover;

    [Header("Weather Controller")]
    public EnvironmentManager enviManager;

    [Header("Social Interaction")]
    public GameObject[] friendWith;
    public GameObject[] avoiding;
    public GameObject[] chasing;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (enviManager.localTemp < 62f || enviManager.localTemp > 82f)
        {
            beziMover.currentState = FollowBeziCurve.FishState.Hide;
        }
        else
        {
            // Temperature is within the range, you may want to handle this case.
        }


    }
}




