using UnityEngine;
using PlayerMechanics;
using EnemyMechanics;
using System.Runtime.CompilerServices;

namespace WeaponMechanics
{
    public class Sword : MonoBehaviour
    {
        //This script makes both the sword and the axe do damage and apply status effects on hit

        public GameObject bloodVFX;
        [SerializeField] private GameObject player;

        private Sonido sound;
        protected Camara myCam;

        public float damage;

        public string stunType;

        public bool candamage;
        public bool hasPlayedSound;

        public Attack ataque;

        protected Scaryness scaryness;
        // Start is called before the first frame update
        void Start()
        {
            myCam = Camara.instance;
            sound = Sonido.instance;
            ataque = player.GetComponent<Attack>();

            scaryness = player.GetComponent<Scaryness>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ataque.candamage == true)
            {
                if (other.gameObject.tag.Equals("Enemy"))
                {     
                    scaryness.IncreaseScaryness(0.3f);

                    Enemy enemigo = other.GetComponent<Enemy>();
                    DealDamage(enemigo);

                    #region Play Sound
                    //this is so the sword only plays the sound of its first impact
                    if (hasPlayedSound == false)
                    {
                        if (enemigo.life - damage <= 0)
                        {
                            sound.playaudio("Strong Sword Impact", null);

                        }
                        else
                        {
                            sound.playaudio("Sword Impact", null);
                        }
                        hasPlayedSound = true;
                    }
                    #endregion

                    if (enemigo.life <= 0) ataque.ActivateSlowMo(0.2f, 0.08f);
                }
                else
                {
                    if (hasPlayedSound == false)
                    {
                        if (other.gameObject.tag.Equals("Damager") || other.gameObject.tag.Equals("Hitbox"))
                        {
                            //This is in case the sword collides with the damagers in the enemy's hands
                            //In this case you get the value of life from the zombie (father) gameobject of the damager

                            myCam.ShakeCam(2);
                            scaryness.IncreaseScaryness(0.3f);

                            Enemy enemigo = other.GetComponent<Hitbox>().myEnemy;
                            if (enemigo.life - damage <= 0)
                            {
                                sound.playaudio("Strong Sword Impact", null);

                                //Matar a un enemigo con la espada aumenta el miedo
                                scaryness.IncreaseScaryness(1);
                            }
                            else
                            {
                                sound.playaudio("Sword Impact", null);
                            }
                            hasPlayedSound = true;
                            if (enemigo.life <= 0) ataque.ActivateSlowMo(0.2f, 0.08f);
                        }
                        else
                        {
                            sound.playaudio("Sword impact wood", null);
                        }
                    }
                }
            }
        }

        protected virtual void DealDamage(Enemy enemigo) { }
    }
}
