using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]
    private float _speedAfterSkipping = 1f;
    [SerializeField]
    private Vector3 _scaleDecreasAfterSkipping = new Vector3(.03f, .03f, .03f);

    private TunelPathCurve _pathCurve;
    private Vector3 _displacement;

    private NoteSpawner _spawner;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Player _player;

    private float _timeToArrived = 3f;
    private float _remaindTimeToArrived;

    private float _timeAfterSkipping = 1f;
    private float _remaindTimeAfterSkipping;

    private bool _isBeforArrived = true;
    private bool _isCatched = false;

    public System.Action<Note> RemoveNote;

    public Vector3 StartPosition
    {
        get => _startPosition;
        set => _startPosition = value;
    }

    public Vector3 EndPosition
    {
        get => _endPosition;
        set => _endPosition = value;
    }
    public NoteSpawner Spawner
    {
        get => _spawner;
        set => _spawner = value;
    }
    public TunelPathCurve PathCurve
    {
        get => _pathCurve;
        set => _pathCurve = value;
    }

    public Vector3 Displacement
    {
        get => _displacement;
        set => _displacement = value;
    }

    public Player Player
    {
        get => _player;
        set => _player = value;
    }

    private void Start()
    {
        _remaindTimeToArrived = _timeToArrived;
        _remaindTimeAfterSkipping = _timeAfterSkipping;
    }

    private void Update()
    {
        if (_isBeforArrived)
        {
            _startPosition = _spawner.transform.position;
            _endPosition = _player.transform.position;

            float percentage = _remaindTimeToArrived / _timeToArrived;
            _remaindTimeToArrived -= Time.deltaTime;
            float interpolatedZ = Mathf.Lerp(_endPosition.z, _startPosition.z, percentage);

            Vector3 newPosition = _pathCurve.GetWayLinePoint(interpolatedZ);
            transform.position = newPosition + _displacement;

            if (_remaindTimeToArrived < 0)
                _isBeforArrived = false;
        } 
        else
        {
            if (_remaindTimeAfterSkipping > 0)
            {
                _remaindTimeAfterSkipping -= Time.deltaTime;

                Vector3 newPosition = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z - _speedAfterSkipping * Time.deltaTime
                );
                transform.position = newPosition;

                Vector3 newScale = transform.localScale - _scaleDecreasAfterSkipping * Time.deltaTime;
                transform.localScale = newScale;
            }
            else
            {
                RemoveNote?.Invoke(this);
            }
        }
    }
}
