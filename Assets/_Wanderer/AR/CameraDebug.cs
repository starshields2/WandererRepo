using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraDebug : MonoBehaviour
{
    public ARCameraManager arCameraManager;

    void Start()
    {
        if (arCameraManager != null)
        {
            // Access the camera and print information
            Camera camera = arCameraManager.GetComponent<Camera>();
            if (camera != null)
            {
                Debug.Log("Connected Camera: " + camera.name);
                Debug.Log("Camera Type: " + camera.GetType());
                Debug.Log("Camera's Current Field of View: " + camera.fieldOfView);
                Debug.Log("Camera's Aspect Ratio: " + camera.aspect);
            }
            else
            {
                Debug.LogWarning("No camera found in ARCameraManager.");
            }
        }
        else
        {
            Debug.LogWarning("ARCameraManager is not assigned!");
        }
    }
}
