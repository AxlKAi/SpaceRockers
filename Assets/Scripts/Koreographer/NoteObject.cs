using UnityEngine.UI;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    //TODO delete all debug
    [SerializeField]
    private Text _debugText;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float timeToArrived = 2f;
    private float remaindTtime;

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
        float percentage = remaindTtime / timeToArrived;
        remaindTtime -= Time.deltaTime;
        Vector3 interpolatedPosition = Vector3.Lerp( EndPosition, StartPosition, percentage);
        transform.position = interpolatedPosition;

        if(_debugText != null)
            _debugText.text = $"{remaindTtime.ToString("F2")}";
    }
}
