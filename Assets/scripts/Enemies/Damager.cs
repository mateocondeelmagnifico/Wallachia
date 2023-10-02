using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    //This goes in the arms of the enemies, it deals damage to the player if it collides with the enemy arm when attacking

    public GameObject father;
    public BasicEnemy myEnemy;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            if (father.GetComponent<BasicEnemyMovement>().candamage == true)
            {
                other.gameObject.GetComponent<Life>().health--;
                father.GetComponent<BasicEnemyMovement>().candamage = false;
            }
        }
    }
    public void dealdamage(float damage, string type, string status)
    {
        //This is for when a bullet collides with the enemy in the arm, it still counts
        myEnemy.takedamage(damage, type, true);
        myEnemy.statuseffect(status);
    }
}
