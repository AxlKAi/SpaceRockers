using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeArray : MonoBehaviour
{
    [SerializeField] private List<GameObject> _circlePoints = new List<GameObject>();
    [SerializeField] private List<GameObject> _circles = new List<GameObject>();

    // StaArt is called before the first frame update
    void Start()
    {
        for (var i = 0; i < _circles.Count; i++)
        {
            _circles[i].transform.position = _circlePoints[i].transform.position;
            _circles[i].transform.rotation = _circlePoints[i].transform.rotation;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
