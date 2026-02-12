using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private float _strafeForce;
    [SerializeField] private float _forwardSpeed;
    
    [Header("Настройки крена")]
    [SerializeField] private float _maxRollTorque = 3f;      // Максимальная сила крена
    [SerializeField] private float _stabilityTorque = 2f;     // Сила возврата в нейтраль
    [SerializeField] private float _maxRollAngle = 60f;       // Максимальный угол

    private Rigidbody _targetBody;
    private Transform _transform;
    private PlayerInput _playerInput;

    private Vector3 _force;
    
    //TODO delete
    //private float _rotateInput = 0;

    //TODO добавить логику поворота корабля
    private float _rotationAngle;

    public Vector3 Force => _force;
    public float RotationAngle => _rotationAngle;

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


        float playerInputX = _playerInput.Controls.x;

        if (playerInputX < -0.01f)
        {
            _rotationAngle = Mathf.MoveTowards(_rotationAngle, _maxRollAngle, _maxRollTorque * Time.fixedDeltaTime);
        }
        else if (playerInputX > 0.01f)
        {
            _rotationAngle = Mathf.MoveTowards(_rotationAngle, -_maxRollAngle, _maxRollTorque * Time.fixedDeltaTime);
        }
        else
        {
            _rotationAngle = Mathf.MoveTowards(_rotationAngle, 0f, _stabilityTorque * Time.fixedDeltaTime);
        }

        _targetBody.transform.eulerAngles = new Vector3(0, 0, _rotationAngle);

        Debug.Log($"Поворот угол={_rotationAngle}");

    }
}
