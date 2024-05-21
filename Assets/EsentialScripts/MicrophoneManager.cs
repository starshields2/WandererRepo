using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class MicrophoneManager : MonoBehaviour
{
    public bool micConnect = false;
    public AudioSource goAudioSource;
   

    // Start is called before the first frame update
    void Start()
    {
        if (Microphone.devices.Length <= 0)
        {
            Debug.Log("Mic not connected!");
        }
        else
        {
            micConnect = true;
        }

        goAudioSource = GetComponent<AudioSource>();

        // Start microphone recording
        goAudioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        goAudioSource.loop = true;
        goAudioSource.volume = 0; // Mute the audio source

        // Wait until the microphone starts recording
        while (!(Microphone.GetPosition(null) > 0))
        {
        }

        goAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (micConnect)
        {
            // Get spectrum data
            float[] spectrumData = new float[256];
            goAudioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

            // Calculate RMS (Root Mean Square)
            float sum = 0;
            for (int i = 0; i < spectrumData.Length; i++)
            {
                sum += spectrumData[i] * spectrumData[i];
            }
            float rmsValue = Mathf.Sqrt(sum / spectrumData.Length);

            // Calculate dB value
            float dbValue = 20 * Mathf.Log10(rmsValue / 0.1f); // Assuming 0.1 is reference level

            Debug.Log("Current microphone level in dB: " + dbValue);
        }
    }
}
