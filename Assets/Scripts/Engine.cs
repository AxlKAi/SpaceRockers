using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private float _strafeForce;
    [SerializeField] private float _forwardSpeed;

    [Header("Настройки крена")]
    [SerializeField] private float _maxRollTorque = 1f;      // Максимальная сила крена
    //TODO delete
    [SerializeField] private float _rollSensitivity = 1f;     // Чувствительность управления
    [SerializeField] private float _stabilityTorque = 20f;     // Сила возврата в нейтраль

    [Header("Пределы")]
    [SerializeField] private float maxRollAngle = 60f;       // Максимальный угол

    private Rigidbody _targetBody;
    private Transform _transform;
    private PlayerInput _playerInput;

    private Vector3 _force;
    private float _rotateInput = 0;

    //TODO добавить логику поворота корабля
    private Vector3 _rotationAngle;

    public Vector3 Force => _force;
    public Vector3 RotationAngle => _rotationAngle;

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

        if(_playerInput.Controls.x < -0.01f)
        {
            _rotateInput = Mathf.MoveTowards(_rotateInput, -1f, _rollSensitivity * Time.fixedDeltaTime);
            _rotationAngle = _targetBody.transform.forward * _rotateInput * _maxRollTorque;

            Debug.Log($"Поворот налево угол={_rotationAngle}");
        } 
        else if (_playerInput.Controls.x > 0.01f)
        {
            _rotateInput = Mathf.MoveTowards(_rotateInput, 1f, _rollSensitivity * Time.fixedDeltaTime);
            _rotationAngle = _targetBody.transform.forward * _rotateInput * _maxRollTorque;

            Debug.Log($"Поворот направо угол={_rotationAngle}");
        }
        else
        {
            _rotateInput = Mathf.MoveTowards(_rotateInput, 0f, _stabilityTorque * 0.5f * Time.fixedDeltaTime);

            angle = Mathf.MoveTowards(_rotateInput, 0f, _stabilityTorque * 0.5f * Time.fixedDeltaTime);
            _rotationAngle = _targetBody.transform.forward * angle * _maxRollTorque;

            Debug.Log($"Возвращаюсь угол={_rotationAngle}");
        }

        // _targetBody.AddTorque(_rotationAngle, ForceMode.Force);

        _targetBody.transform.localEulerAngles = _rotationAngle;

    }    
}
