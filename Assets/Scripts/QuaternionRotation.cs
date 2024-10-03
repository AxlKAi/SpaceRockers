using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private Transform _axis;

    private Transform _transform;
    private float _step = 0;
    private Quaternion _startRotation;

    [ProPlayButton]
    public void Rotate()
    {
        // Метод создает кватернион при умножении на который будет поворот на нужный нам угол
        // вокруг заданной оси. Причем ось может быть любой!!!
        Quaternion quaternion = Quaternion.AngleAxis(_angle, _axis.forward);
        quaternion = Quaternion.Slerp(quaternion, _transform.rotation, Mathf.Clamp01( _step));
        _transform.rotation = quaternion * _startRotation;
    }

    private void Update()
    {
        _step += .002f;
        Rotate();
        Debug.Log($"step = {_step}");
    }

    private void Awake()
    {
        _transform = transform;
        _startRotation = transform.rotation;
    }

    private void OnDrawGizmos()
    {
        if (_axis == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _axis.forward * 10f);
        Gizmos.DrawRay(transform.position, -_axis.forward * 10f);
    }
}
