using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private AudioPeer _audioPeer;

    [Range (5000f, 5000000f)]
    [SerializeField] private float _cubeScaleFactor = 10000f;

    private List<GameObject> _cubes = new List<GameObject>();

    private int _cubesCount = 64;
    private float _cubesRadius = 100f;
    private float _degreesOfArc = 360f;
    private Transform _parrentGameObject;
    private Vector3 _cubeScaleMatrix = new Vector3(1, 1, 1);


    // Start is called before the first frame update
    void Start()
    {
        float cubesStep = (float)System.Math.Round ( _degreesOfArc / _cubesCount, 4);
        _cubes.Clear();
        _parrentGameObject = gameObject.transform;

        for(int i=0; i<_cubesCount; i++)
        {
            GameObject cube = Instantiate(_cubePrefab);
            cube.transform.position = gameObject.transform.position;
            cube.transform.parent = _parrentGameObject;
            cube.name = "Cube" + i;
            this.transform.eulerAngles = Vector3.right * cubesStep * i;
            cube.transform.position = Vector3.up * _cubesRadius;
            _cubes.Add(cube);
        }        
    }

    private void FixedUpdate()
    {
        for(int i=0; i<_cubesCount; i++)
        {
            if (_cubes[i] != null)
            {
                _cubes[i].transform.localScale = new Vector3(2 + _audioPeer.Samples[i] * _cubeScaleFactor, 7, 7);
            }                
        }
    }
    void Update()
    {
        
    }
}
