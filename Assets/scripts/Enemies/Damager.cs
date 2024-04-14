using UnityEngine;
using PlayerMechanics;

namespace EnemyMechanics
{
    public class Damager : MonoBehaviour
    {
        //This goes in the arms of the enemies, it deals damage to the player if it collides with the enemy arm when attacking

        public GameObject father;
        public Enemy myEnemy;
        [SerializeField] private float extraDmage;
        private void Start()
        {
            myEnemy = father.GetComponent<Enemy>();
        }

        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                if (myEnemy.canDamage == true)
                {
                    other.gameObject.GetComponent<Life>().ChangeLife(-1 - extraDmage);
                    myEnemy.canDamage = false;
                }
            }
        }
      
    }
}
