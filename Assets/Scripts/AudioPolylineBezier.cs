using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AudioPolylineBezier : AudioPolyline
{
    [SerializeField]
    private float _smoothingLength = 2f;
    [SerializeField]
    private int _smoothingSections = 5;

    private BezierCurve[] _curves;

    //TODO amplitudeAmpfiler перенести в клас _audioPeer ??
    public override void SetLinePointsFromAudioSource()
    {
        base.SetLinePointsFromAudioSource();

        for (int i = 0; i < _curves.Length; i++)
        {
            Vector3 position = _lineRenderer.GetPosition(i);
            Vector3 lastPosition = i == 0 ? _lineRenderer.GetPosition(0) : _lineRenderer.GetPosition(i - 1);
            Vector3 nextPosition = _lineRenderer.GetPosition(i + 1);

            Vector3 lastDirection = (position - lastPosition).normalized;
            Vector3 nextDirection = (nextPosition - position).normalized;

            Vector3 startTangent = (lastDirection + nextDirection) * _smoothingLength;
            Vector3 endTangent = (nextDirection + lastDirection) * -1 * _smoothingLength;

            _curves[i].Points[0] = position; // Start Position (P0)
            _curves[i].Points[1] = position + startTangent; // Start Tangent (P1)
            _curves[i].Points[2] = nextPosition + endTangent; // End Tangent (P2)
            _curves[i].Points[3] = nextPosition; // End Position (P3)
        }

        int index = 0;
        _lineRenderer.positionCount = _curves.Length * _smoothingSections;

        for (int i = 0; i < _curves.Length; i++)
        {
            Vector3[] segments = _curves[i].GetSegments(_smoothingSections);

            for (int j = 0; j < segments.Length; j++)
            {
                _lineRenderer.SetPosition(index, segments[j]);
                index++;
            }
        }
    }

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        if (_lineRenderer == null)
            Debug.LogError("Cant find LineRenderer component.");

        if (_audioPeer == null)
            Debug.LogError("Can`t find AudioPeer component.");

        _lineRenderer.positionCount = _linesCount;

        _curves = new BezierCurve[_lineRenderer.positionCount - 1];

        for (int i = 0; i < _curves.Length; i++)
        {
            _curves[i] = new BezierCurve();
        }

        SetLinePointsFromAudioSource();
    }
}
