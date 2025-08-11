using UnityEngine.UI;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    //TODO delete all debug
    [SerializeField]
    private Text _debugText;
    [SerializeField]
    private float _speedWhileDieng = 1f;
    [SerializeField]
    private float _timeToDie = 1f;
    [SerializeField]
    private float _scaleDecreasWhileDie = .93f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float timeToArrived = 3f;
    private float remaindTtime;
    private bool _isBeforArrived = true; 

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

    // Start is called before the first frame update
    private void Start()
    {
        remaindTtime = timeToArrived;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isBeforArrived)
        {
            float percentage = remaindTtime / timeToArrived;
            remaindTtime -= Time.deltaTime;
            Vector3 interpolatedPosition = Vector3.Lerp( EndPosition, StartPosition, percentage);
            transform.position = interpolatedPosition;

            if (remaindTtime < 0)
                _isBeforArrived = false;

            if(_debugText != null)
                _debugText.text = $"{remaindTtime.ToString("F2")}";
        }
        else
        {
            if(_timeToDie > 0)
            {
                _timeToDie -= Time.deltaTime;
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
                Destroy(gameObject);
            }
        }
    }
}
