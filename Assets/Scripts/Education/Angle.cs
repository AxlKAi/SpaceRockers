using UnityEngine;

public class Angle : MonoBehaviour
{
    [SerializeField] private SimpleVector _vectorOne;
    [SerializeField] private SimpleVector _vectorTwo;

    void Update()
    {
        if (_vectorOne == null || _vectorTwo == null)
            return;

        gameObject.name = $"angle = {Vector3.SignedAngle(_vectorOne.Vector, _vectorTwo.Vector, Vector3.up):F2}";
    }
}
