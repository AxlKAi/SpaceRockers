using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float[] _samples = new float[64];
    public float[] Samples { get { return _samples; } }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.time = 85;
        _audioSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetSpectrumAudioSource();
    }

    private void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
}
