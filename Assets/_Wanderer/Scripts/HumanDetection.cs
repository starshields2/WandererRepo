using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class HumanDetection : MonoBehaviour
{
    public ARFaceManager arFace;
    public FollowBeziCurve[] yBass;
    public bool face;
    public bool faceDetected;
    public TextMeshProUGUI facedetectinfo;

    private void OnEnable()
    {
        if (arFace != null)
            arFace.facesChanged += OnFacesChanged;
    }

    private void OnDisable()
    {
        if (arFace != null)
            arFace.facesChanged -= OnFacesChanged;
    }

    private void OnFacesChanged(ARFacesChangedEventArgs args)
    {
        // A face was added
        if (args.added.Count > 0)
        {
            Debug.Log("New face detected!");
            faceDetected = true;
            face = true;
            facedetectinfo.text = "FACE";

            foreach (FollowBeziCurve fish in yBass)
            {
                fish.currentState = FollowBeziCurve.FishState.Hide;
            }
        }

        // All faces removed
        if (args.removed.Count > 0 && arFace.trackables.count == 0)
        {
            Debug.Log("No faces detected.");
            faceDetected = false;
            face = false;
            facedetectinfo.text = "NO FACE";

            foreach (FollowBeziCurve fish in yBass)
            {
                fish.currentState = FollowBeziCurve.FishState.RegularSwim;
            }
        }
    }
}
