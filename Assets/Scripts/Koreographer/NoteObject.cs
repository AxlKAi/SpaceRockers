using UnityEngine.UI;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField]
    private float _speedWhileDieng = 1f;
    [SerializeField]
    private float _timeToDie = 1f;
    [SerializeField]
    private float _scaleDecreasWhileDie = .93f;
    [SerializeField]
    private float _scaleDecreasWhenCatched = .85f;
    

    private SpawnByEvent _spawner; 
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _timeToArrived = 3f;
    private float _remaindTime;
    private float _remaindTimeToDie;
    private bool _isBeforArrived = true;
    private bool _isCatched = false;

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

    public SpawnByEvent Spawner
    {
        get => _spawner;
        set => _spawner = value;
    }

    public float GetRemainingTime()
    {
        float time =  0;

        if (_isBeforArrived)
            time = _remaindTime;
        else
            time = _timeToDie - _remaindTimeToDie;

        return time;
    }

    public void Catched()
    {
        _isCatched = true;
        _isBeforArrived = false;
    }


    private void Start()
    {
        _remaindTime = _timeToArrived;
        _remaindTimeToDie = _timeToDie;
    }

    private void Update()
    {
        if (_isBeforArrived)
        {
            float percentage = _remaindTime / _timeToArrived;
            _remaindTime -= Time.deltaTime;
            Vector3 interpolatedPosition = Vector3.Lerp( EndPosition, StartPosition, percentage);
            transform.position = interpolatedPosition;

            if (_remaindTime < 0)
                _isBeforArrived = false;
        }
        else if(_isCatched)
        {
            if (_remaindTimeToDie > 0)
            {
                _remaindTimeToDie -= Time.deltaTime;

                Vector3 newPosition = new Vector3(
                    transform.position.x,
                    transform.position.y + _speedWhileDieng * Time.deltaTime,
                    transform.position.z
                );
                transform.position = newPosition;

                Vector3 newScale = transform.localScale * _scaleDecreasWhenCatched;
                transform.localScale = newScale;
            }
            else
            {
                _spawner.DestroyNote(this, gameObject);
            }
        }
        else
        {
            if(_remaindTimeToDie > 0)
            {
                _remaindTimeToDie -= Time.deltaTime;

                Vector3 newPosition = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z - _speedWhileDieng * Time.deltaTime
                );
                transform.position = newPosition;

                Vector3 newScale = transform.localScale * _scaleDecreasWhileDie;
                transform.localScale = newScale;
            }
            else
            {
                _spawner.DestroyNote(this, gameObject);
            }
        }
    }
}
