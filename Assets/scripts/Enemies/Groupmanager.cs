using UnityEngine;

namespace EnemyMechanics
{
    public class Groupmanager : MonoBehaviour
    {
        //Enemies come in groups, this script makes it so that they all attack the player if one is alerted
        //It also drags the player and hitmarker gameobject to each enemy, so i dont have to drag it to each individual enemy
        // remember dragging the enemies to the group manager
        public string enemytype;
        public bool activated;
        public bool isattacking;
        public int currentball;

        public GameObject player;

        void Start()
        {
            //This is so that all enemies in the stage get this things
            for (int count = transform.childCount; count > 0; count--)
            {
                transform.GetChild(count - 1).GetComponent<Enemy>().player = player;
                transform.GetChild(count - 1).GetComponent<Enemy>().groupManager = this;
            }
        }

        private void Update()
        {
            //Only for werewolves
            if(enemytype == "Werewolf" && activated)
            {
                bool atacking = false;
                bool destroySelf = true;

                for(int i = 0; i < transform.childCount; i++)
                {
                    //Check if there are werewolves attacking
                    if(transform.GetChild(i).GetComponent<WereWolf>().enabled)
                    {
                        if(transform.GetChild(i).GetComponent<WereWolf>().angry) atacking = true;
                    }
                }

                if (!atacking)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        //Send werewolf to attack

                        if (transform.GetChild(i).GetComponent<WereWolf>().enabled && !atacking)
                        {
                            transform.GetChild(i).GetComponent<WereWolf>().SendToPlayer();
                        }
                    }
                }

                for (int i = 0; i < transform.childCount; i++)
                {
                    //Check if there are any living werewolves

                    if (transform.GetChild(i).GetComponent<WereWolf>().enabled)
                    {
                        destroySelf = false;
                    }
                }

                if (destroySelf)
                {
                    Destroy(this);
                }
            }
        }

        //This is to prevent enemies from straying too far
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                other.GetComponent<Enemy>().returnHome(transform.position);
            }
        }

        public void InformEnemies()
        {
            activated = true;

            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Enemy>().playerDetected = true;
            }
        }
    }
}
