using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") && other.name != "Damage collider left" && other.name != "Damage collider right")
        {
            other.GetComponent<Enemy>().transforming += Time.deltaTime/3;
            other.GetComponent<Enemymovement>().isinholy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") && other.name != "Damage collider left" && other.name != "Damage collider right")
        {
            other.GetComponent<Enemymovement>().isinholy = false;
        }
    }
}
