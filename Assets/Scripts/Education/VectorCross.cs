using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorCross : MonoBehaviour
{
    [SerializeField] private SimpleVector _vectorOne;
    [SerializeField] private SimpleVector _vectorTwo;

    [SerializeField] private SimpleVector _resultVector;

    private void Update()
    {
        _resultVector.SetVector(Vector3.Cross(_vectorOne.Vector, _vectorTwo.Vector));
    }
}
