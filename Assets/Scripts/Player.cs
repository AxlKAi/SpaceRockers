using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstantForce))]
public class Player : MonoBehaviour
{
    [SerializeField] private Engine _engine;
    [SerializeField] private float _constantForcePower;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private ConstantForce _constantForce;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = gameObject.AddComponent<PlayerInput>();
        _engine.Initialize(_rigidbody);
        _constantForce = GetComponent<ConstantForce>();
    }

    private void Update()
    {
        if (_playerInput != null)
        {
           _constantForce.force = Vector3.right * _playerInput.Controls.x * _constantForcePower;
        }
    }
}
