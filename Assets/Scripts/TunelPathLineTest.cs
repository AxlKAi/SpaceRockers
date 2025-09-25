using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunelPathLineTest : MonoBehaviour
{
    // TODO delete this class

    [SerializeField]
    private TunelPathCurve _line;
    [SerializeField]
    private Vector3 _displacement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = _line.GetWayLinePoint(transform.position.z) + _displacement;        

        Vector3 lookAt = Vector3.zero;
        Vector3 newPosition = _line.GetWayLinePointAndLookAt(transform.position.z, out lookAt);
        transform.LookAt(lookAt + _displacement);
        transform.position = newPosition + _displacement;
    }
}
