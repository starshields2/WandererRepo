using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GenARManager : MonoBehaviour
{
    public AROcclusionManager occlusionManager;
    // Start is called before the first frame update
    void Start()
    {
        if (occlusionManager.descriptor?.supportsEnvironmentDepthImage ?? false)
        {
            // If depth mode is available on the user's device, perform
            // the steps you want here.
            Debug.Log("Depth API is supported on this device.");
            // You can perform additional setup or logic here.
        }
        else
        {
            Debug.Log("Depth API is not supported on this device.");
            // You may want to handle the case where depth API is not supported.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
