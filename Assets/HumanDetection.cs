using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;

public class HumanDetection : MonoBehaviour
{
    public ARFaceManager arFace;
    public FollowBeziCurve[] yBass;

    private void OnEnable()
    {
        if (arFace != null)
        {
            arFace.facesChanged += OnFacesChanged;
        }
    }

    private void OnDisable()
    {
        if (arFace != null)
        {
            arFace.facesChanged -= OnFacesChanged;
        }
    }

    private void OnFacesChanged(ARFacesChangedEventArgs args)
    {
       foreach(FollowBeziCurve fish in yBass)
        {
            fish.currentState = FollowBeziCurve.FishState.Run;
        }
        foreach (var addedFace in args.added)
        {
            // Run your custom code here when a face is detected
            Debug.Log("A new face is detected!");
         
        }
    }
}
