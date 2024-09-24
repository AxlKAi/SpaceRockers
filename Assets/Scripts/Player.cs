using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private Engine _engine;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = gameObject.AddComponent<PlayerInput>();
        _engine.Initialize(_rigidbody);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
