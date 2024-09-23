using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AudioPolyline : MonoBehaviour
{
    [SerializeField]
    protected AudioPeer _audioPeer;
    [SerializeField]
    protected float _amplitudeAmpfiler = 500f;
    [SerializeField]
    protected float _lineLength = 7f;
    [SerializeField]
    protected float _timeToLive = 4f;
    [SerializeField]
    protected float _amplitudeDropLength = .3f;
    [SerializeField]
    protected float _moveSpeed = -2;
    [SerializeField]
    protected bool _amplitudeDrop = false;

    protected LineRenderer _lineRenderer;
    protected int _linesCount = 60;
    protected float _positionZ = 0;

    //TODO amplitudeAmpfiler перенести в клас _audioPeer ??
    public virtual void SetLinePointsFromAudioSource()
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

        if(_amplitudeDrop == true)
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
