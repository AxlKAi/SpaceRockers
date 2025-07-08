using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForvard : MonoBehaviour
{
    [SerializeField]
    private Vector3 _speed = new Vector3(0,0,0);

    private Vector3 _position;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _position = _transform.position;
        _position = _position + _speed;
        _transform.position = _position;
    }
}
