using UnityEngine;
using PlayerMechanics;
using EnemyMechanics;

namespace WeaponMechanics
{
    public class Sword : MonoBehaviour
    {
        //This script makes both the sword and the axe do damage and apply status effects on hit

        public GameObjectgetter getter;
        public GameObject bloodVFX;
        GameObject player;
        GameObject soundmanager;

        Sonido sound;
        private Camara myCam;

        public float axedamage, damage;

        public string stunType;

        public bool candamage, isAxe;
        public bool hasPlayedSound;

        public Attack ataque;

        private Scaryness scaryness;
        // Start is called before the first frame update
        void Start()
        {
            myCam = Camara.instance;
            player = getter.Player;
            soundmanager = getter.Soundmanager;
            sound = soundmanager.GetComponent<Sonido>();
            axedamage = 2;
            ataque = player.GetComponent<Attack>();

            scaryness = player.GetComponent<Scaryness>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ataque.candamage == true)
            {
                if (other.gameObject.tag.Equals("Enemy"))
                {
                    myCam.ShakeCam(2);
                    scaryness.IncreaseScaryness(0.3f);

                    Enemy enemigo = other.GetComponent<Enemy>();

                    #region Play Sound
                    //this is so the sword only plays the sound of its first impact
                    if (hasPlayedSound == false)
                    {
                        if (enemigo.life - damage <= 0)
                        {
                            sound.playaudio("Strong Sword Impact");

                        }
                        else
                        {
                            sound.playaudio("Sword Impact");
                        }
                        hasPlayedSound = true;
                    }
                    #endregion

                    #region Apply Damage and Effects
                    if (isAxe == false)
                    {
                        //sword deals more damage based on missing health
                        damage = (1 + (enemigo.maxLife - enemigo.life) / 1.5f) + (scaryness.howScary / 8);

                        enemigo.TakeDamage(damage, "Light", true);
                        enemigo.StatusEffect("Iron");
                        bloodVFX.GetComponent<ParticleSystem>().Emit(100);
                    }
                    else
                    {
                        damage = axedamage;
                        enemigo.TakeDamage(axedamage + (scaryness.howScary / 8), "Heavy", true);
                        enemigo.StatusEffect("Iron");
                        bloodVFX.GetComponent<ParticleSystem>().Emit(10000);
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
                                sound.playaudio("Strong Sword Impact");

                                //Matar a un enemigo con la espada aumenta el miedo
                                scaryness.IncreaseScaryness(1);
                            }
                            else
                            {
                                sound.playaudio("Sword Impact");
                            }
                            hasPlayedSound = true;
                            if (enemigo.life <= 0) ataque.ActivateSlowMo(0.2f, 0.08f);
                        }
                        else
                        {
                            sound.playaudio("Sword impact wood");
                        }
                    }
                }
            }
        }
    }
}
