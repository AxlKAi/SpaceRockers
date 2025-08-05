using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float timeToArrived = 3f;
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
        remaindTtime -= Time.deltaTime;
        Vector3 interpolatedPosition = Vector3.Lerp( EndPosition, StartPosition, remaindTtime / timeToArrived);
        transform.position = interpolatedPosition;
    }
}
