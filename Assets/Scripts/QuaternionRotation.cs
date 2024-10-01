using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private Vector3 _axis;

    private Transform _transform;

    [ProPlayButton]
    public void Rotate()
    {
        // ����� ������� ���������� ��� ��������� �� ������� ����� ������� �� ������ ��� ����
        // ������ �������� ���. ������ ��� ����� ���� �����!!!
        Quaternion quaternion = Quaternion.AngleAxis(_angle, _axis);

        _transform.rotation = quaternion * _transform.rotation;
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDrawGizmos()
    {
        if (_axis == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _axis * 10f);
        Gizmos.DrawRay(transform.position, -_axis * 10f);
    }
}
