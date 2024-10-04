using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour
{
    [SerializeField] private SimpleVector _vectorOne;
    [SerializeField] private SimpleVector _vectorTwo;

    // Update is called once per frame
    void Update()
    {
        if (_vectorOne == null || _vectorTwo == null)
            return;

        gameObject.name = $"angle = {Vector3.Angle(_vectorOne.Vector, _vectorTwo.Vector, Vector3.up):F2}";
    }
}
