using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve
{
    public Vector3[] Points;

    public BezierCurve()
    {
        Points = new Vector3[4];
    }

    public BezierCurve(Vector3[] points)
    {
        Points = points;
    }

    public Vector3 StartPosition
    {
        get
        {
            return Points[0];
        }
    }

    public Vector3 EndPosition
    {
        get
        {
            return Points[3];
        }
    }

    public Vector3 GetSegment(float time)
    {
        time = Mathf.Clamp01(time);
        float timeLeft = 1 - time;

        return (timeLeft * timeLeft * timeLeft * Points[0])
            + (3 * timeLeft * timeLeft * time * Points[1])
            + (3 * timeLeft * time * time * Points[2])
            + (time * time * time * Points[3]);
    }

    public Vector3[] GetSegments(int subdividions)
    {
        float time;
        Vector3[] segments = new Vector3[subdividions];

        for(int i=0; i<subdividions; i++)
        {
            time = (float)i / subdividions;
            segments[i] = GetSegment(time);
        }

        return segments;
    }
}
