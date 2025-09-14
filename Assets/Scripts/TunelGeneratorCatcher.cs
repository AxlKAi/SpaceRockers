using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunelGeneratorCatcher : MonoBehaviour
{
    public System.Action<TunelWall> OnTunelWallTrigger;

    void OnTriggerEnter(Collider other)
    {
        TunelWall wall = other.GetComponentInParent<TunelWall>();

        if(wall != null)
        {
            OnTunelWallTrigger?.Invoke(wall);
        }
    }
}
