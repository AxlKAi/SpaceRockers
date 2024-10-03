using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDot : MonoBehaviour
{
    [SerializeField]
    private SimpleVector _vectorOne;

    [SerializeField]
    private SimpleVector _vectorTwo;

    private void OnDrawGizmos()
    {
        if (_vectorOne == null || _vectorTwo == null)
            return;

        name = "Dot = " + Vector3.Dot(_vectorOne.Vector, _vectorTwo.Vector).ToString("F2");
    }
}
