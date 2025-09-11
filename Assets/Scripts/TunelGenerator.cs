using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _generatingPoint;
    [SerializeField]
    private GameObject _catcherPoint;
    [SerializeField]
    private float _step = 100f;
    [SerializeField]
    private GameObject _gatePrefab;
    [SerializeField]
    private LineRendererSmoother _wayLine;

    private float _lastSpawnedZ;
    private Vector3[] _wayLinePoints;

    private void Start()
    {
        StartTunel();
    }

    private void StartTunel()
    {
        if(_generatingPoint == null || _catcherPoint == null)
        {
            Debug.LogError("Generators points not set up");
            return;
        }

        if(_gatePrefab == null)
        {
            Debug.LogError("Gate prefab not set");
            return;
        }

        if (_gatePrefab == null)
        {
            Debug.LogError("Smoother line component not set");
            return;
        }

        int numPositions = _wayLine.Line.positionCount;
        _wayLinePoints = new Vector3[numPositions];
        _wayLine.Line.GetPositions(_wayLinePoints);

        _lastSpawnedZ = _catcherPoint.transform.position.z;

        while (_lastSpawnedZ <= _generatingPoint.transform.position.z)
        {
            Vector3 spawnedPosition = GetWayLinePoint();

            Instantiate(_gatePrefab, spawnedPosition, Quaternion.identity);
            _lastSpawnedZ += _step;
        }
    }

    private Vector3 GetWayLinePoint()
    {
        if (_wayLinePoints == null)
        {
            Debug.LogError("No points in _wayLinePoints");
            return _generatingPoint.transform.position;
        }

        Vector3 way = Vector3.zero;
        int nearIndex = 0;

        for(int i = 0; i<_wayLinePoints.Length; i++)        
        {
            if(_wayLinePoints[i].z > _lastSpawnedZ)
            {
                nearIndex = i;
                break;
            }
        }

        if(nearIndex > 0)
        {
            float length = _wayLinePoints[nearIndex].z - _wayLinePoints[nearIndex - 1].z;
            float k = _wayLinePoints[nearIndex].z - _lastSpawnedZ;
            float ratio = k / length;

            way = Vector3.Lerp(_wayLinePoints[nearIndex], _wayLinePoints[nearIndex-1], ratio);

            Debug.Log($"Point{nearIndex} coord:{_wayLinePoints[nearIndex]}   point{nearIndex - 1} coord:{_wayLinePoints[nearIndex - 1]}");
            Debug.Log($"For Z={_lastSpawnedZ} ratio = {ratio}  point={way}");
        }
        else
        {
            way = _wayLinePoints[0];
        }

        return way;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
