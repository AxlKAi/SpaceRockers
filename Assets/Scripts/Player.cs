using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConstantForce))]
public class Player : MonoBehaviour
{
    [SerializeField] private Engine _engine;
    [SerializeField] private Camera _camera;

    [SerializeField] private float _cameraDistance;
    [SerializeField] private float _cameraSlideTime;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private ConstantForce _constantForce;
    private PlayerInput _playerInput;

    private Vector3 _cameraSlideVelocity = Vector3.zero;

    public PlayerInput PlayerInput
    {
        get => _playerInput;
    }

    public void OnPlayerReward(int points)
    {
        Debug.Log("player reward " + points);
    }

    public void OnPlayerMissed()
    {
        Debug.Log("Player missed");
    }

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = gameObject.AddComponent<PlayerInput>();
        _engine.Initialize(_rigidbody, _playerInput);
        _constantForce = GetComponent<ConstantForce>();

        _playerInput.CatchLeftPressed += KeyPressTest;
        _playerInput.CatchMiddlePressed += KeyPressTest;
        _playerInput.CatchRightPressed += KeyPressTest;
    }

    private void Update()
    {
        if (_playerInput != null)
        {
           _constantForce.force = _engine.Force;
        }

        if(_camera != null)
        {
            var cameraSmoothDistance = Vector3.SmoothDamp(_camera.transform.position, _transform.position, ref _cameraSlideVelocity, _cameraSlideTime);

            var cameraPosition = new Vector3(
                cameraSmoothDistance.x,
                cameraSmoothDistance.y,
                _transform.position.z - _cameraDistance
            );

            _camera.transform.position = cameraPosition;
        }
    }

    private void KeyPressTest()
    {
        //TODO delete this method

        Debug.Log($"Left catch pressed at time {Time.realtimeSinceStartup}");
    }
}
