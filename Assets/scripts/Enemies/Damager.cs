using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    //This goes in the arms of the enemies, it deals damage to the player if it collides with the enemy arm when attacking

    public GameObject father;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            if (father.GetComponent<Enemymovement>().candamage == true)
            {
                other.gameObject.GetComponent<Life>().health--;
                father.GetComponent<Enemymovement>().candamage = false;
            }
        }
    }
    public void dealdamage(float damage, string type, string status)
    {
        //This is for when a bullet collides with the enemy in the arm, it still counts
        father.GetComponent<Enemy>().takedamage(damage, type);
        father.GetComponent<Enemy>().statuseffect(status);
    }
}
