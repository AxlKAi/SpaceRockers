using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLine : MonoBehaviour
{
    [SerializeField] private Transform _lineEnd;

    private void OnDrawGizmos()
    {
        if (_lineEnd == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, _lineEnd.position);
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
