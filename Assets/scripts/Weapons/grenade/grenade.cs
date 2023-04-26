using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    //The script for the grenade before it explodes
    //Its the same for garlic and silver grenades

    public GameObject[] grenadetype;
    public weapons armas;
    public GameObject[] type;

    Rigidbody cuerporigido;

    public Sonido sound;
    void Start()
    {
        //this is so that it doen't drop to your feet when spawned
        cuerporigido = GetComponent<Rigidbody>();
        cuerporigido.AddForce(transform.forward * 450);
        cuerporigido.AddForce(transform.up * 150);
    }

    // Update is called once per frame
    void Update()
    {
        if (armas.currentgrenade == 0)
        {
            type[0].SetActive(true);
        }
        else
        {
            type[1].SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //When it collides with anything the grenade explodeas an instate the explosion that corresponds to the grenade type
        GameObject.Instantiate(grenadetype[armas.currentgrenade], transform.position, transform.rotation);
        if (armas.currentgrenade == 1)
        {
            sound.playaudio("Garlic Grenade");
        }
        
        Destroy(this.gameObject);
    }
}
