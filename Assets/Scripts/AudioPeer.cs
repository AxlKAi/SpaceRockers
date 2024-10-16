using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    //TODO AudioPeer must be an singlTone 

    [SerializeField] private AudioSource _audioSource;

    public const int AudioCurveDetalization = 512;
    public const int FrequiencyBandCount = 64;

    private float[] _samples = new float[AudioCurveDetalization];
    private float[] _frequiencyBand = new float[FrequiencyBandCount];

    public float[] Samples { get { return _samples; } }
    public float[] FrequiencyBand { get { return _frequiencyBand; } }

    public int FrequiencyBandCountGetter { get { return FrequiencyBandCount; } }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.time = 85; // TODO delete rewind and replay
        _audioSource.Play();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        GetSpectrumAudioSource();
        CalculateFrequencyBand();
    }

    private void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    private void CalculateFrequencyBand()
    {
        int frequencyStep = AudioCurveDetalization / FrequiencyBandCount;

        for (int i=0; i<FrequiencyBandCount; i++)
        {
            float average = 0;

            for (int j=0; j< frequencyStep; j++)
            {
                average += _samples[i * frequencyStep + j];
            }

            average /= frequencyStep;
            _frequiencyBand[i] = average;
        }
    }
}
