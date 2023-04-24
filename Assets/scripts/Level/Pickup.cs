using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public string pickuptype;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (pickuptype == "Rifle")
            {
                collision.gameObject.GetComponent<weapons>().hasrifle = true;
                collision.gameObject.GetComponent<weapons>().playsound();
                Destroy(this.gameObject);
            }
            if (pickuptype == "Axe")
            {
                collision.gameObject.GetComponent<weapons>().hasaxe = true;
                collision.gameObject.GetComponent<weapons>().playsound();
                Destroy(this.gameObject);
            }
            if (pickuptype == "Bullet")
            {
                collision.gameObject.GetComponent<weapons>().hasbullet = true;
                collision.gameObject.GetComponent<weapons>().playsound();
                Destroy(this.gameObject);
            }
            if (pickuptype == "Grenade")
            {
                collision.gameObject.GetComponent<weapons>().hasgrenade = true;
                collision.gameObject.GetComponent<weapons>().playsound();
                Destroy(this.gameObject);
            }
        }
    }
}
