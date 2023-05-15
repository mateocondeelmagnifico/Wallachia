using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnposition : MonoBehaviour
{
    weapons armas;
    RespawnManager manager;
    void Start()
    {
        armas = GetComponent<weapons>();

        if (FindObjectOfType<RespawnManager>() != null) 
        {
            manager = FindObjectOfType<RespawnManager>();
            transform.position = manager.spawnposition;
            Debug.Log("yes");
            Debug.Log("manager");
            armas.hasaxe = manager.hasaxe;
            armas.hasrifle = manager.hasrifle;
            armas.hasbullet = manager.hasbullets;
            armas.hasgrenade = manager.hasgrenades;
        }
        else
        {
            Debug.Log("no");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
