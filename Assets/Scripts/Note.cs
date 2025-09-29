using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]
    private Vector3 _scaleDecreasAfterSkipping = new (.03f, .03f, .03f);

    private TunelPathCurve _pathCurve;
    private Vector3 _displacement;

    private NoteSpawner _spawner;
    private Vector3 _startPosition;
    private Vector3 _targetPosition; 
    private GameObject _endPosition;
    private Player _player;

    private float _timeToArrived = 3f;
    private float _remaindTimeToArrived;

    private float _timeAfterSkipping = .5f;
    private float _remaindTimeAfterSkipping;

    private bool _isBeforArrived = true;
    private bool _isCatched = false;

    public System.Action<Note> RemoveNote;

    //TODO delete it for debuging
    public string SpawnerName;
    public int Index;

    public Vector3 StartPosition
    {
        get => _startPosition;
        set => _startPosition = value;
    }

    public Vector3 TargetPosition
    {
        get => _targetPosition;
        set => _targetPosition = value;
    }

    public GameObject EndPosition
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

    public void ResetTimers()
    {
        _isBeforArrived = true;
        _isCatched = false;
        _remaindTimeToArrived = _timeToArrived;
        _remaindTimeAfterSkipping = _timeAfterSkipping;
    }

    public void Caught()
    {
        _isCatched = true;
    }

    private void Start()
    {
        ResetTimers();
    }

    private void Update()
    {
        if (_isBeforArrived)
        {
            _startPosition = _spawner.transform.position;
            _targetPosition = _spawner.MusicSheet.transform.position;

            float percentage = _remaindTimeToArrived / _timeToArrived;
            _remaindTimeToArrived -= Time.deltaTime;
            float interpolatedZ = Mathf.Lerp(_targetPosition.z, _startPosition.z, percentage);

            Vector3 newPosition = _pathCurve.GetWayLinePoint(interpolatedZ);
            transform.position = newPosition + _displacement;

            if (_remaindTimeToArrived < 0)
                _isBeforArrived = false;
        } 
        else if (_isCatched)
        {
            //TODO catch note animation

            if (_remaindTimeAfterSkipping > 0)
            {
                _remaindTimeAfterSkipping -= Time.deltaTime;
                float percentage = _remaindTimeAfterSkipping / _timeAfterSkipping;
                
                _targetPosition = _spawner.MusicSheet.transform.position + _displacement;

                Vector3 newPosition = Vector3.Lerp(
                    _player.transform.position,
                    _targetPosition,
                    percentage);

                transform.position = newPosition;

                Vector3 newScale = transform.localScale - _scaleDecreasAfterSkipping * Time.deltaTime;
                transform.localScale = newScale;
            }
            else
            {
                RemoveNote?.Invoke(this);
            }
        }
        else
        {
            if (_remaindTimeAfterSkipping > 0)
            {
                _remaindTimeAfterSkipping -= Time.deltaTime;
                float percentage = _remaindTimeAfterSkipping / _timeAfterSkipping;

                _targetPosition = _spawner.MusicSheet.transform.position + Displacement;

                Vector3 newPosition = Vector3.Lerp(
                    _endPosition.transform.position + Displacement, 
                    _targetPosition, 
                    percentage);

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
