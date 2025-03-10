using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMechanics;

public class HolyGround : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") && other.name != "Damage collider left" && other.name != "Damage collider right")
        {
            other.GetComponent<Enemy>().regeneration -= Time.deltaTime/3;
            other.GetComponent<Enemy>().slowCondition = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") && other.name != "Damage collider left" && other.name != "Damage collider right")
        {
            other.GetComponent<Enemy>().slowCondition = false;
        }
    }
}
