using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunelPathLine : MonoBehaviour
{
    //TODO сделать три метода, 
    //GetWayLinePoint один возвращает координату по параметру Z
    //GetWayLineLookAt второй LookAt координату по параметру Z
    //GetWayLinePointAndLookAt третий оба значения

    [SerializeField]
    private LineRendererSmoother _wayLine;
    private Vector3[] _wayLinePoints;

    private void Start()
    {
        if (_wayLine == null)
        {
            Debug.LogError("Smoother line component not set");
            return;
        }
        else
        {
            int numPositions = _wayLine.Line.positionCount;

            _wayLinePoints = new Vector3[numPositions];
            _wayLine.Line.GetPositions(_wayLinePoints);
        }
    }

    public Vector3 GetWayLinePoint(float zCoordinate)
    {
        Vector3 centerPoint = Vector3.zero;

        if (_wayLinePoints == null)
        {
            Debug.LogError("No points in _wayLinePoints");
            return centerPoint;
        }

        int nearIndex = 0;

        for (int i = 0; i < _wayLinePoints.Length; i++)
        {
            if (_wayLinePoints[i].z > zCoordinate)
            {
                nearIndex = i;
                break;
            }
        }

        if (nearIndex > 0)
        {
            float length = _wayLinePoints[nearIndex].z - _wayLinePoints[nearIndex - 1].z;
            float k = _wayLinePoints[nearIndex].z - zCoordinate;
            float ratio = k / length;

            centerPoint = Vector3.Lerp(_wayLinePoints[nearIndex], _wayLinePoints[nearIndex - 1], ratio);
        }
        else
        {
            centerPoint = _wayLinePoints[0];
        }

        return centerPoint;
    }

    public Vector3 GetWayLinePointAndLookAt(float zCoordinate, out Vector3 lookAtPoint) 
    {
        Vector3 centerPoint = Vector3.zero;
        lookAtPoint = Vector3.zero;

        if (_wayLinePoints == null)
        {
            Debug.LogError("No points in _wayLinePoints");
            return lookAtPoint;
        }

        int nearIndex = 0;

        for (int i = 0; i < _wayLinePoints.Length; i++)
        {
            if (_wayLinePoints[i].z > zCoordinate)
            {
                nearIndex = i;
                break;
            }
        }

        if (nearIndex > 1)
        {
            float length = _wayLinePoints[nearIndex].z - _wayLinePoints[nearIndex-1].z;
            float k = _wayLinePoints[nearIndex].z - zCoordinate;
            float ratio = k / length;

            centerPoint = Vector3.Lerp(_wayLinePoints[nearIndex], _wayLinePoints[nearIndex - 1], ratio);

            if (nearIndex > 2 && ratio > .7)
            {
                lookAtPoint = _wayLinePoints[nearIndex - 2];
            }
            else
            {
                lookAtPoint = _wayLinePoints[nearIndex - 1];
            }
        }
        else
        {
            centerPoint = _wayLinePoints[0];
        }

        return centerPoint;
    }

    public Vector3 GetWayLineLookAt(float zCoordinate)
    {
        Vector3 lookAtPoint = Vector3.zero;

        if (_wayLinePoints == null)
        {
            Debug.LogError("No points in _wayLinePoints");
            return lookAtPoint;
        }

        int nearIndex = 0;

        for (int i = 0; i < _wayLinePoints.Length; i++)
        {
            if (_wayLinePoints[i].z > zCoordinate)
            {
                nearIndex = i;
                break;
            }
        }

        if (nearIndex > 1)
        {
            lookAtPoint = _wayLinePoints[nearIndex - 1];
        }
        else
        {
            lookAtPoint = _wayLinePoints[0];
        }

        return lookAtPoint;
    }

    /*
    public Vector3 GetWayLineAngle(float zCoordinate)
    {
        return
    }
    */
}
