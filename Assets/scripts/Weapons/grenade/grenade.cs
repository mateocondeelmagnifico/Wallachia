using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    public GameObject[] grenadetype;
    public weapons armas;
    public GameObject[] type;

    Rigidbody cuerporigido;

    public Sonido sound;
    void Start()
    {
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
        GameObject.Instantiate(grenadetype[armas.currentgrenade], transform.position, transform.rotation);
        if (armas.currentgrenade == 1)
        {
            sound.playaudio("Garlic Grenade");
        }
        
        Destroy(this.gameObject);
    }
}
