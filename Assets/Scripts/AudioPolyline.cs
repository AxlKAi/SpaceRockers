using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AudioPolyline : MonoBehaviour
{
    [SerializeField]
    private AudioPeer _audioPeer;

    private LineRenderer _lineRenderer;
    private int _linesCount = 60;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        if (_lineRenderer == null)
            Debug.LogError("Cant find LineRenderer component.");

        if (_audioPeer == null)
            Debug.LogError("Can`t find AudioPeer component.");

        _lineRenderer.positionCount = _linesCount;

        DrawAudioline();
    }

    void FixedUpdate()
    {
        
    }

    private void DrawAudioline(float[] Samples, float amplitudeAmpfiler, float length)
    {
        for (int i = 0; i < _linesCount; i++)
        {
            _lineRenderer.SetPosition(i, new Vector3(i * length, Samples[i]*amplitudeAmpfiler, 0));
        }
    }
}
