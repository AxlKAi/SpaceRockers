using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        _camera.transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
    }
}
