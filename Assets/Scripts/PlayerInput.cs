using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // ��������� ���� ���������������, � � PlayerInput ����� ������ ��������� ����� ����������

    private Vector2 _moveInput; // ������ ���������� �� ����������

    public Action CatchPressed;  // ������� �� ������� �������
    public Action CatchReleased;
    public Action CatchLeftPressed;
    public Action CatchRightPressed;
    public Action CatchMiddlePressed;

    public Vector2 Controls => _moveInput;

    private void Update()
    {
        _moveInput = Vector2.up * Input.GetAxis("Vertical") + Vector2.right * Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
            CatchPressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            CatchReleased?.Invoke();

        if (Input.GetKeyDown(KeyCode.I))
            CatchLeftPressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.O))
            CatchMiddlePressed?.Invoke();

        if (Input.GetKeyDown(KeyCode.P))
            CatchRightPressed?.Invoke();
    }
}
