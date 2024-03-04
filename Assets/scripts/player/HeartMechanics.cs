using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponMechanics;

namespace PlayerMechanics
{
    public class HeartMechanics : MonoBehaviour
    {
        public float heartsAmount;
        private float animTimer;

        private bool isOnAnim;

        public GameObject heart, gun1, gun2;
        private Attack attackScript;
        private Shooting gunScript1, gunScript2, currentGunScript;
        private Life lifeScript;

        private void Start()
        {
            attackScript = GetComponent<Attack>();
            gunScript1 = gun1.GetComponent<Shooting>();
            gunScript2 = gun2.GetComponent<Shooting>();
            lifeScript = GetComponent<Life>();
        }

        void Update()
        {
            if(animTimer > 0)
            {
                animTimer -= Time.deltaTime;
            }
            else if(isOnAnim)
            {
                lifeScript.ChangeLife(3);
                attackScript.ispaused = false;
                currentGunScript.isInAnim = false;
                heart.SetActive(false);
                isOnAnim = false;
            }
        }

        public void TryConsumeHeart()
        {
            #region Check if can Heal
            if (heartsAmount <= 0) return;

            if(gun1.activeInHierarchy) currentGunScript = gunScript1;
            else currentGunScript = gunScript2;

            if (currentGunScript.reloading || attackScript.attacking) return;
            #endregion

            currentGunScript.isInAnim = true;
            attackScript.ispaused = true;

            heart.SetActive(true);
            animTimer = 2;
            isOnAnim = true;
        }
    }
}
