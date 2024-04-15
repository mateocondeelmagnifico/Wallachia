using UnityEngine;
using WeaponMechanics;

public class Pickup : MonoBehaviour
{
    //Unlocks different weapons
    //It rally is unblocking the capacity to change weapons by changing a bool in the weapons script
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
