using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnposition : MonoBehaviour
{
    void Start()
    {
        if (FindObjectOfType<RespawnManager>() != null) 
        {
            transform.position = FindObjectOfType<RespawnManager>().spawnposition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
