using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
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
        father.GetComponent<Enemy>().takedamage(damage, type);
        father.GetComponent<Enemy>().statuseffect(status);
    }
}
