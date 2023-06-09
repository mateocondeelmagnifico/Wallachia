using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnposition : MonoBehaviour
{
    public GameObject player;
    weapons armas;
    public RespawnManager manager;

    bool haswaited;
    void Start()
    {
        armas = GetComponent<weapons>();
        if (FindObjectOfType<RespawnManager>() != null) 
        {
            manager = FindObjectOfType<RespawnManager>();
            player.transform.position = manager.spawnposition;

            armas.hasaxe = manager.hasaxe;
            armas.hasrifle = manager.hasrifle;
            armas.hasbullet = manager.hasbullets;
            armas.hasgrenade = manager.hasgrenades;

            armas.currentgrenade = manager.currentgrenade;
            armas.currentmeleeweapon = manager.currentmelee;
            armas.currentrangeweapon = manager.currentgun;

            armas.SetiInactive();
            armas.equippoint();
            
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
