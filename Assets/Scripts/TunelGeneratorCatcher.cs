using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunelGeneratorCatcher : MonoBehaviour
{
    public System.Action<TunelWall> OnTunelWallTrigger;

    private void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        OnTunelWallTrigger.Invoke(new TunelWall());
    }
}
