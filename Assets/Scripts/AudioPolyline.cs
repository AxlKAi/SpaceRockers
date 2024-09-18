using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AudioPolyline : MonoBehaviour
{
    [SerializeField]
    private AudioPeer _audioPeer;
    [SerializeField]
    private float _amplitudeAmpfiler = 500f;
    [SerializeField]
    private float _lineLength = 7f;
    [SerializeField]
    private float _timeToLive = 4f;
    [SerializeField]
    private float _amplitudeDropLength = .3f;
    [SerializeField]
    private float _moveSpeed = -2;

    private LineRenderer _lineRenderer;
    private int _linesCount = 60;
    private float _positionZ = 0;

    //TODO amplitudeAmpfiler перенести в клас _audioPeer ??
    public void SetLinePointsFromAudioSource()
    {
        for (int i = 0; i < _linesCount; i++)
        {
            _lineRenderer.SetPosition(i, new Vector3(i * _lineLength, _audioPeer.Samples[i]*_amplitudeAmpfiler, _positionZ));
        }
    }

    public void SetAudioPeer(AudioPeer audioPeer)
    {
        _audioPeer = audioPeer;
    }

    public void SetPositionZ(float positionZ)
    {
        _positionZ = positionZ;
    }

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        if (_lineRenderer == null)
            Debug.LogError("Cant find LineRenderer component.");

        if (_audioPeer == null)
            Debug.LogError("Can`t find AudioPeer component.");

        _lineRenderer.positionCount = _linesCount;
        SetLinePointsFromAudioSource();
    }

    private void FixedUpdate()
    {
        _timeToLive -= Time.fixedDeltaTime;

        if (_timeToLive < 0)
            Destroy(gameObject);

        AmplitudeDrop();
    }

    private void AmplitudeDrop()
    {
        Vector3[] points = new Vector3[_lineRenderer.positionCount];

        _lineRenderer.GetPositions(points);

        for (int i=0; i<points.Length; i++)
        {
            if (points[i].y > 0)
                points[i].y -= _amplitudeDropLength;

            points[i].z += _moveSpeed; 
        }

        _lineRenderer.SetPositions(points);
    }
}
