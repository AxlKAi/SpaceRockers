using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private Transform _axis;

    private Transform _transform;

    [ProPlayButton]
    public void Rotate()
    {
        Quaternion quaternion = Quaternion.AngleAxis(_angle, _axis.forward);

        _transform.rotation = quaternion * _transform.rotation;
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Awake()
    {
        _transform = transform;

        if (_axis == null)
        {
            Debug.LogError("No axis setup for rotation. Defaul transform using.");
            _axis = _transform;
        }
    }

    private void OnDrawGizmos()
    {
        if (_axis == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _axis.forward * 10f);
        Gizmos.DrawRay(transform.position, -_axis.forward * 10f);
    }
}
