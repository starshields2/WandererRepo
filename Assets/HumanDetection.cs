using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;

public class HumanDetection : MonoBehaviour
{
    public ARFaceManager arFace;
    public FollowBeziCurve[] yBass;

    [SerializeField] // This will allow you to manually edit the field in the Inspector during runtime
    public bool faceDetected;
    public bool face;

    void Start()
    {
        // Initialize or any other setup needed
    }

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
        // If there are newly added faces, set faceDetected to true
        if (args.added.Count > 0)
        {
            face = true; // A face has been detected
            Debug.Log("New face detected!");
        }
        else
        {
            face = false; // No faces detected, set faceDetected to false
            Debug.Log("No faces detected.");
        }

        // If faces are removed, set faceDetected to false
        if (args.removed.Count > 0)
        {
            //faceDetected = false;
            Debug.Log("Face removed");
        }
    }

    // Method to manually set the value of faceDetected during runtime (useful for testing)
    public void SetFaceDetected(bool value)
    {
        faceDetected = value;
        Debug.Log("faceDetected manually set to: " + faceDetected);
    }
}
