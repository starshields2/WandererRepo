using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class HumanDetection : MonoBehaviour
{
    public ARFaceManager arFace;
    public YellowBass[] yBass;

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
       foreach(YellowBass fish in yBass)
        {
            fish.currentState = YellowBass.FishState.Run;
        }
        foreach (var addedFace in args.added)
        {
            // Run your custom code here when a face is detected
            Debug.Log("A new face is detected!");
         
        }
    }
}
