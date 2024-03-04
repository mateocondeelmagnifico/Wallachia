using UnityEngine;
using PlayerMechanics;

namespace EnemyMechanics
{
    public class Damager : MonoBehaviour
    {
        //This goes in the arms of the enemies, it deals damage to the player if it collides with the enemy arm when attacking

        public GameObject father;
        public Enemy myEnemy;

        private Camara playerCam;

        private void Start()
        {
            playerCam = Camara.instance;
            myEnemy = father.GetComponent<Enemy>();
        }

        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                if (myEnemy.canDamage == true)
                {
                    playerCam.ShakeCam(1);
                    other.gameObject.GetComponent<Life>().ChangeLife(-1);
                    myEnemy.canDamage = false;
                }
            }
        }
        public void dealdamage(float damage, string type, string status)
        {
            //This is for when a bullet collides with the enemy in the arm, it still counts
            myEnemy.TakeDamage(damage, type, true);
            myEnemy.StatusEffect(status);
        }
    }
}
