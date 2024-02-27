using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMechanics;

namespace WeaponMechanics
{
    public class Cross : MonoBehaviour
    {
        //This script is in the cross collider
        public float crossStrength;

        public bool isActive;

        public Light myLight;

        public GameObject cross;

        private void Start()
        {
            Deactivate();
        }
        private void Update()
        {
            if (isActive)
            {
                crossStrength -= Time.deltaTime;
                myLight.intensity = crossStrength * 12;
                if (crossStrength <= 0)
                {
                    Deactivate();
                }
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
                Vector3 direction = enemyPosition - cross.transform.position;
                Ray rayo = new Ray(cross.transform.position, direction);
                RaycastHit hit;
                #endregion

                Debug.DrawRay(cross.transform.position, direction);


                if (Physics.Raycast(rayo, out hit, 2000, 1 << 8 | 1 << 11 | 1 << 0))
                {
                    if (hit.collider.gameObject.tag.Equals("Enemy"))
                    {
                        hit.collider.gameObject.GetComponent<BasicEnemy>().statuseffect("Holy");
                    }
                }
            }
        }

        public void Activate()
        {
            cross.GetComponent<MeshRenderer>().enabled = true;
            isActive = true;
        }
        public void Deactivate()
        {
            cross.GetComponent<MeshRenderer>().enabled = false;
            isActive = false;
        }
    }
}
