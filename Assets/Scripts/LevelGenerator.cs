using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LevelGenerator : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer.SetPosition(0,Vector3.zero);
        _lineRenderer.SetPosition(1, new Vector3(10,10,7000));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
