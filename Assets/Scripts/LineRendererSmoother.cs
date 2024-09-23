using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LineRendererSmoother : MonoBehaviour
{
    public LineRenderer Line;
    public Vector3[] InitialState;
    public float SmoothingLength = 2f; // длинна до опорных точек
    public int SmoothingSections = 10;
}
