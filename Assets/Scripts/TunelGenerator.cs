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
    [SerializeField]
    private GameObject _parentTunelActor;

    private float _lastSpawnedZ;
    private Vector3[] _wayLinePoints;

    private void Start()
    {
        CheckupInitParametrs();

        StartTunel();
    }

    private void CheckupInitParametrs()
    {
        if (_generatingPoint == null || _catcherPoint == null)
        {
            Debug.LogError("Generators points not set up");
            return;
        }

        if (_gatePrefab == null)
        {
            Debug.LogError("Gate prefab not set");
            return;
        }

        if (_gatePrefab == null)
        {
            Debug.LogError("Smoother line component not set");
            return;
        }

        if (_parentTunelActor == null)
        {
            Debug.LogError("No parent actor for instantiating tunel prefabs selected");
            return;
        }
    }

    private void StartTunel()
    {
        int numPositions = _wayLine.Line.positionCount;
        Vector3 lookAtPoint;
        _wayLinePoints = new Vector3[numPositions];
        _wayLine.Line.GetPositions(_wayLinePoints);

        _lastSpawnedZ = _catcherPoint.transform.position.z;

        while (_lastSpawnedZ <= _generatingPoint.transform.position.z)
        {
            Vector3 spawnedPosition = GetWayLinePoint(out lookAtPoint);

            var actor = Instantiate(_gatePrefab, spawnedPosition, Quaternion.identity);
            actor.transform.LookAt(lookAtPoint);
            actor.transform.parent = _parentTunelActor.transform;

            _lastSpawnedZ += _step;
        }
    }

    private Vector3 GetWayLinePoint(out Vector3 lookAtPoint)
    {
        lookAtPoint = Vector3.zero;

        if (_wayLinePoints == null)
        {
            Debug.LogError("No points in _wayLinePoints");
            return _generatingPoint.transform.position;
        }

        Vector3 centerPoint = Vector3.zero;
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
            lookAtPoint = _wayLinePoints[nearIndex - 1];

            float length = _wayLinePoints[nearIndex].z - lookAtPoint.z;
            float k = _wayLinePoints[nearIndex].z - _lastSpawnedZ;
            float ratio = k / length;

            centerPoint = Vector3.Lerp(_wayLinePoints[nearIndex], lookAtPoint, ratio);

            //TODO remove debug
            //Debug.Log($"Point{nearIndex} coord:{_wayLinePoints[nearIndex]}   point{nearIndex - 1} coord:{lookAtPoint}");
            //Debug.Log($"For Z={_lastSpawnedZ} ratio = {ratio}  point={centerPoint}");
        }
        else
        {
            centerPoint = _wayLinePoints[0];
        }

        return centerPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
