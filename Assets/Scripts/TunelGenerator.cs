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
    private LineRendererSmoother _smoother;

    private float _lastSpawnedZ;
    private Vector3[] _smootherPoints;

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
        
        _smoother.Line.GetPositions(_smootherPoints);

        _lastSpawnedZ = _catcherPoint.transform.position.z;

        while (_lastSpawnedZ <= _generatingPoint.transform.position.z)
        {
            Vector3 spawnedPosition = new Vector3(transform.position.x, transform.position.y, _lastSpawnedZ);
            
            Instantiate(_gatePrefab, spawnedPosition, Quaternion.identity);
            _lastSpawnedZ += _step;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
