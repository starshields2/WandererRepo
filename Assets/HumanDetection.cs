using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;

public class HumanDetection : MonoBehaviour
{
    public ARFaceManager arFace;
    public FollowBeziCurve[] yBass;
    public bool faceDetected = false;

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
        // If there are newly added faces, we set faceDetected to true
        if (args.added.Count > 0)
        {
            faceDetected = false;
            Debug.Log("New face detected!");
        }
        else
        {
            faceDetected = false; // Optionally, set it to false if you want to reset when no faces are detected.
        }

        // You can also handle removed faces if needed
        if (args.removed.Count > 0)
        {
            faceDetected = false;
            Debug.Log("Face removed");
        }
    }


    [ContextMenu("FaceDetect")]
    public void FaceDetect()
    {
        faceDetected = !faceDetected;
    }
}
