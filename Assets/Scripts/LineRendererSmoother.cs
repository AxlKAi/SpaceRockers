using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO ���� ���� �� ����� I dont need this class, its only for editor (and folder EDITOR also)
// remove it from project

[RequireComponent(typeof(LineRenderer))]
public class LineRendererSmoother : MonoBehaviour
{
    public LineRenderer Line;
    public Vector3[] InitialState;
    public float SmoothingLength = 2f; // ������ �� ������� �����
    public int SmoothingSections = 10;
    public GameObject[] GameObjects;
}
