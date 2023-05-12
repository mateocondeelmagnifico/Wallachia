using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresh : MonoBehaviour
{
    RespawnManager spawnmanager;
    // Refills HP and Ammo
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.GetComponent<Life>().health = 6;
            collision.GetComponent<Life>().riflereloaded = false;
            collision.GetComponent<Life>().pistolreloaded = false;
            GameObject.Find("Main Camera").GetComponent<Throwinggrenade>().remaininggarlic = 2;
            GameObject.Find("Main Camera").GetComponent<Throwinggrenade>().remainingsilver = 2;

            spawnmanager = FindObjectOfType<RespawnManager>();
            spawnmanager.spawnposition = transform.position;
            Destroy(this.gameObject);
        }
    }
}
