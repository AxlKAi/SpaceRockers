using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private float _altitude;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _spherecastRadius;
    [SerializeField] private float _maxDistance;

    private Rigidbody _rigidbody;
    private Transform _transform;

    public void Initialize(Rigidbody targetBody)
    {
        _transform = transform;
        _rigidbody = targetBody;
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null)
            return;
    }
}
