using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private float _strafeForce;
    [SerializeField] private float _forwardSpeed;

    private Rigidbody _targetBody;
    private Transform _transform;
    private PlayerInput _playerInput;

    private Vector3 _force;

    public Vector3 Force => _force;

    public void Initialize(Rigidbody targetBody, PlayerInput playerInput)
    {
        _transform = transform;
        _targetBody = targetBody;
        _playerInput = playerInput;
    }

    private void FixedUpdate()
    {
        if (_targetBody == null && _playerInput == null)
            return;

        _force = new Vector3(
            _playerInput.Controls.x * _strafeForce,
            _playerInput.Controls.y * _strafeForce, 
            _forwardSpeed);            

         Debug.Log($"force = {_force} ");
    }
}
