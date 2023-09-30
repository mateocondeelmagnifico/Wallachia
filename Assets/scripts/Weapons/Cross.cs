using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    //This script is in the cross collider
    public float crossStrength;

    public bool isActive;

    public Light myLight;


    private void Update()
    {
        if (isActive && crossStrength > 0)
        {
            crossStrength -= Time.deltaTime;
            myLight.intensity = crossStrength * 12;
        }

        if (!isActive)
        {
            if (crossStrength < 3)
            {
                crossStrength += Time.deltaTime;
            }
            myLight.intensity = 0;
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            //Acess Enemy script
        }
    }
}
