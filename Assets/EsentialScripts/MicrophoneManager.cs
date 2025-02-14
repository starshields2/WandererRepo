using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;

public class MicrophoneManager : MonoBehaviour
{
    [Header("Microphone")]
    public bool micConnect = false;
    public string selectedDevice;
    public TextMeshProUGUI device;
    public BlueGill[] blueGills;
    public YellowBass[] yBass;
    public LongNoseGar[] LNG;
    public Turtle[] turts;

    public bool BGActive = true;

    private int sampleWindow = 128; // Window size for calculating dB (can be adjusted)
    private AudioClip micClip;


    [Header("Text Items")]
    public TextMeshProUGUI db;
    public float textDb;

    // Start is called before the first frame update
    void Start()
    {
        BGActive = true;

        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // The user authorized use of the microphone.
            Debug.Log("has authorized");
        }
        else
        {
            // We do not have permission to use the microphone.
            // Ask for permission or proceed without the functionality enabled.
       
            Permission.RequestUserPermission(Permission.Microphone);

        }
    

        if (Microphone.devices.Length <= 0)
        {
            Debug.Log("Mic not connected!");
        }
        else
        {
            micConnect = true;
            selectedDevice = Microphone.devices[0];
            Debug.Log("Microphone: " + selectedDevice);
            device.text = selectedDevice.ToString();
        }

        if (micConnect)
        {
            // Start microphone recording with looping, but without playing through an AudioSource
            micClip = Microphone.Start(selectedDevice, true, 10, 44100);

            // Wait until the microphone starts recording
            while (!(Microphone.GetPosition(selectedDevice) > 0)) { }

            Debug.Log("Microphone recording started.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (micConnect)
        {
            textDb = GetRelativeDecibelLevel();
        }
        db.text = "DB: " + textDb;

        // If the dB level is greater than or equal to 85, deactivate BGActive
        if (textDb >= 85)
        {
            Debug.Log("should hide");
            BGActive = false;
        }
        if (textDb < 85)
        {
            BGActive = true;
        }
    }

    public void SetBG()
    {
        
    }


    // Function to calculate the decibel level of the microphone input
    // Function to calculate the decibel level of the microphone input and map it to a relative scale (0-140)
    float GetRelativeDecibelLevel()
    {
        float[] audioData = new float[sampleWindow];

        // Get the audio data directly from the Microphone
        int micPosition = Microphone.GetPosition(selectedDevice) - sampleWindow + 1;
        if (micPosition < 0) return 0f; // If there's no data yet, return 0 as silence

        micClip.GetData(audioData, micPosition); // Get microphone data

        float sum = 0f;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += audioData[i] * audioData[i]; // Square the audio data values
        }

        float rmsValue = Mathf.Sqrt(sum / sampleWindow); // Root mean square (RMS)
        float dBValue = 20 * Mathf.Log10(rmsValue / 0.1f); // Convert RMS to dB

        if (float.IsInfinity(dBValue)) dBValue = -80f; // Handle silent audio

        // Map dB values (-80 to 0 or higher) to a relative range (0 to 140)
        float relativeDb = Mathf.Clamp(dBValue + 80, 0, 140); // Shift -80 dB to 0 and clamp to 140 dB max

        return relativeDb;
    }

}
