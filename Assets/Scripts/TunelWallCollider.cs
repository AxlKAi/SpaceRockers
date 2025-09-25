using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunelWallCollider : MonoBehaviour
{
    //TODO ?? по моему этот класс не нужен

    [SerializeField]
    private TunelWall _wall;

    // Start is called before the first frame update
    void Start()
    {
        if(_wall == null)
        {
            Debug.LogError("Collider doesnt have root wall actor");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var wall = other.GetComponentInParent<TunelWall>();

        if(wall != null)
        {
            Debug.Log("trigger wall");
        }
        else
        {
            Debug.Log("trigger");
        }
        //Destroy(other.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
