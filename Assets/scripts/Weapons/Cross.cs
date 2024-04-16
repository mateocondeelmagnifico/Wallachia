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
        private float crossTimer;

        public bool isActive;

        public Light myLight;
        public MeshRenderer crossRenderer;

        public GameObject cross;


        private void Start()
        {
            Deactivate();
        }
        private void Update()
        {
            if (isActive)
            {
                crossTimer -= Time.deltaTime;

                if(crossTimer < 1)
                {
                    crossStrength += Time.deltaTime * 10;
                }
                else
                {
                    crossStrength += Time.deltaTime;
                }

                myLight.intensity = crossStrength * 12;

                if (crossTimer <= 0)
                {
                    Deactivate();
                }
            }

            if (!isActive)
            {
                if (myLight.intensity > 0) myLight.intensity -= Time.deltaTime * 160;
                else crossRenderer.enabled = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!isActive) return;

            if (other.gameObject.tag.Equals("Enemy"))
            {

                #region Define Raycast Values
                Vector3 enemyPosition = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
                Vector3 direction = enemyPosition - cross.transform.position;
                Ray rayo = new Ray(cross.transform.position, direction);
                RaycastHit hit;
                #endregion

                if (Physics.Raycast(rayo, out hit, 2000, 1 << 8 | 1 << 11 | 1 << 0))
                {
                    if (hit.collider.gameObject.tag.Equals("Enemy"))
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().StatusEffect("Holy");
                    }
                }
            }
        }

        public void Activate()
        {
            crossRenderer.enabled = true;
            isActive = true;
            crossStrength = 1;           
            crossTimer = 2;
        }
        public void Deactivate()
        {
            isActive = false;
        }
    }
}
