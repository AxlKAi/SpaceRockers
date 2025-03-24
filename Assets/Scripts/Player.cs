using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstantForce))]
public class Player : MonoBehaviour
{
    [SerializeField] private Engine _engine;
    [SerializeField] private Camera _camera;

    [SerializeField] private float _cameraDistance;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private ConstantForce _constantForce;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = gameObject.AddComponent<PlayerInput>();
        _engine.Initialize(_rigidbody, _playerInput);
        _constantForce = GetComponent<ConstantForce>();
    }

    private void Update()
    {
        if (_playerInput != null)
        {
           _constantForce.force = _engine.Force;
        }

        if(_camera != null)
        {
            var cameraPosition = new Vector3(
                _camera.transform.position.x,
                _camera.transform.position.y,
                _transform.position.z - _cameraDistance
            );

            _camera.transform.position = cameraPosition;
        }
    }
}
