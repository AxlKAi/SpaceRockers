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

    private Rigidbody _targetBody;
    private Transform _transform;

    public void Initialize(Rigidbody targetBody)
    {
        _transform = transform;
        _targetBody = targetBody;
    }

    private void FixedUpdate()
    {
        if (_targetBody == null)
            return;

        var forward = _transform.forward;

        if( Physics.SphereCast(_transform.position, _spherecastRadius, forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            float distance = hitInfo.distance;
            var calcDistance = Mathf.Clamp(distance, _altitude - 10f, _altitude +10f);   // Зажимаем высоту для расчета силы

            var forceFactor  = calcDistance.Remap(_altitude - 10f, _altitude + 10f, _maxForce, 0);  // и ремапим ее на _maxForce
                                                                    // тут обратить внимание что ремап идет на перевернутые значения, и это прекрасно работате

            var force = -forward * forceFactor; 
            _targetBody.AddForce( force, ForceMode.Force);
            Debug.Log($"force = {force}   distance = {calcDistance}   forceFactor = {forceFactor}");
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
