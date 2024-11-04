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

    private int sampleWindow = 128; // Window size for calculating dB (can be adjusted)
    private AudioClip micClip;


    [Header("Text Items")]
    public TextMeshProUGUI db;
    public float textDb;

    // Start is called before the first frame update
    void Start()
    {

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
           // Debug.Log("Current dB level: " + textDb);
        }
        db.text = "DB: " + textDb;

        if (textDb > 85)
        {
            // Loop through the array of BlueGill objects and set their state to Hide
            foreach (var blueGill in blueGills)
            {
                Debug.Log("Hide Bluegill");
                blueGill.beziMover.currentState = FollowBeziCurve.FishState.Hide;
            }
            foreach (var yellowBass in yBass)
            {
                Debug.Log("Hide Bluegill");
                yellowBass.beziMover.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        else
        {
            // If decibel is not above 75, ensure all fish are in Swim state
            foreach (var blueGill in blueGills)
            {
                blueGill.beziMover.currentState = FollowBeziCurve.FishState.RegularSwim;
            }
            foreach (var yellowBass in yBass)
            {
                Debug.Log("Hide YBass");
                yellowBass.beziMover.currentState = FollowBeziCurve.FishState.RegularSwim;
            }
        }

        if (textDb > 65)
        {
            // Loop through the array of BlueGill objects and set their state to Hide
            foreach (var lnGar in LNG)
            {
                Debug.Log("Hide LNG");
                lnGar.beziMover.currentState = FollowBeziCurve.FishState.Hide;
            }
            foreach (var turtle in turts)
            {
                Debug.Log("Hide Bluegill");
                turtle.beziMover.currentState = FollowBeziCurve.FishState.Hide;
            }
        }
        else
        {
            // If decibel is not above 75, ensure all fish are in Swim state
            foreach (var lnGar in LNG)
            {
                lnGar.beziMover.currentState = FollowBeziCurve.FishState.RegularSwim;
            }
            foreach (var turtle in turts)
            {
                Debug.Log("Hide YBass");
                turtle.beziMover.currentState = FollowBeziCurve.FishState.RegularSwim;
            }
        }
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
