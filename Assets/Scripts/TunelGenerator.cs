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
    private TunelPathCurve _wayLine;
    [SerializeField]
    private GameObject _parentTunelActor;

    private float _lastSpawnedZ;
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
        Vector3 spawnedPosition = _wayLine.GetWayLinePointAndLookAt(_lastSpawnedZ ,out lookAtPoint);

        var actor = _pool.Get();
        actor.transform.position = spawnedPosition;
        actor.transform.LookAt(lookAtPoint);
        actor.transform.parent = _parentTunelActor.transform;
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
