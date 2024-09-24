using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private float _altitude;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _spherecastRadius;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody _rigidbody;
    private Transform _transform;

    public void Initialize(Rigidbody targetBody)
    {
        _transform = transform;
        _rigidbody = targetBody;
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null)
            return;

        var forward = _transform.forward;

        if( Physics.SphereCast(_transform.position, _spherecastRadius, forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {

        }
    }

    private void OnDrawGizmos()
    {
        var startPoint = transform.position;
        var endPoint = transform.position + transform.forward * _maxDistance;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(startPoint, Vector3.one * 2f);
        Gizmos.DrawLine(startPoint, endPoint);
        Gizmos.DrawSphere(endPoint, _spherecastRadius);
    }
}
