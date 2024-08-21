using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponMechanics
{
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
            cuerporigido.AddForce(transform.forward * 750);
            cuerporigido.AddForce(transform.up * 150);
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (armas.currentEquip[2] == 0)
            {
                type[0].SetActive(true);
            }
            else
            {
                type[1].SetActive(true);
            }
            */
        }
        private void OnCollisionEnter(Collision collision)
        {
            /*
            //When it collides with anything the grenade explodeas an instate the explosion that corresponds to the grenade type
            GameObject.Instantiate(grenadetype[armas.currentEquip[2]], transform.position, transform.rotation);
            if (armas.currentEquip[2] == 1)
            {
                sound.playaudio("Garlic Grenade");
            }
            else
            {
                sound.playaudio("Smoke Grenade");
            }

            Destroy(this.gameObject);
            */
        }
    }
}
