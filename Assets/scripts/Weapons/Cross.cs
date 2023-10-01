using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    //This script is in the cross collider
    public float crossStrength;

    public bool isActive;

    public Light myLight;

    public Transform crossPosition;

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
        if (other.gameObject.tag.Equals("Enemy") && isActive)
        {
            
            #region Define Raycast Values
            Vector3 enemyPosition = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
            Vector3 direction = enemyPosition - crossPosition.position;
            Ray rayo = new Ray(crossPosition.position, direction);
            RaycastHit hit;
            #endregion

            Debug.DrawRay(crossPosition.position, direction);
            

            if(Physics.Raycast(rayo, out hit, 2000, 1<< 8 | 1 << 11 | 1<< 0))
            {
                if (hit.collider.gameObject.tag.Equals("Enemy"))
                {
                    //Acess Enemy script
                    Debug.Log("Enemy hit");
                }
            }
        }
    }
}
