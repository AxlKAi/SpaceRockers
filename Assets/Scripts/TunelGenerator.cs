using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TunelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _generatingPoint;
    [SerializeField]
    private TunelGeneratorCatcher _catcherPoint;
    [SerializeField]
    private float _step = 100f;
    [SerializeField]
    private TunelWall _gatePrefab;
    [SerializeField]
    private LineRendererSmoother _wayLine;
    [SerializeField]
    private GameObject _parentTunelActor;

    private float _lastSpawnedZ;
    private Vector3[] _wayLinePoints;
    private ObjectPool<TunelWall> _pool;

    private void Start()
    {
        _pool = new ObjectPool<TunelWall>(
            createFunc: () => Instantiate(_gatePrefab), 
            actionOnGet: (obj) => obj.transform.gameObject.SetActive(true), 
            actionOnRelease: (obj) => obj.transform.gameObject.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false,
            defaultCapacity: 45, 
            maxSize: 50);

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
        else
        {
            _catcherPoint.OnTunelWallTrigger += CatchGatePrefab;
        }

        if (_gatePrefab == null)
        {
            Debug.LogError("Gate prefab not set");
            return;
        }

        if (_wayLine == null)
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
        
        _wayLinePoints = new Vector3[numPositions];
        _wayLine.Line.GetPositions(_wayLinePoints);

        _lastSpawnedZ = _catcherPoint.transform.position.z;

        while (_lastSpawnedZ <= _generatingPoint.transform.position.z)
        {
            SpawnWallPrefab();
            _lastSpawnedZ += _step;
        }
    }

    private void SpawnWallPrefab()
    {
        Vector3 lookAtPoint;
        Vector3 spawnedPosition = GetWayLinePoint(out lookAtPoint);

        var actor = _pool.Get();
        actor.transform.position = spawnedPosition;
        actor.transform.LookAt(lookAtPoint);
        actor.transform.parent = _parentTunelActor.transform;
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
        }
        else
        {
            centerPoint = _wayLinePoints[0];
        }

        return centerPoint;
    }

    private void CatchGatePrefab(TunelWall wall)
    {
        _pool.Release(wall);
    }

    private void FixedUpdate()
    {
        if(_generatingPoint.transform.position.z - _lastSpawnedZ > _step)
        {
            SpawnWallPrefab();
            _lastSpawnedZ += _step;
        }
    }
}
