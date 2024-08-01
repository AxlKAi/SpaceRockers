using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEqalizer : MonoBehaviour
{
    [SerializeField] private AudioPeer _audioPeer;
    [SerializeField] private int _bandNumber;

    private void FixedUpdate()
    {
        if (_audioPeer != null)
        {
            transform.localScale = new Vector3(1, 1 + _audioPeer.FrequiencyBand[_bandNumber] * 20000, 1);
        }        
    }
}
