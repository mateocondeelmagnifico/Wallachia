using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //This script makes both the sword and the axe do damage and apply status effects on hit

    public GameObjectgetter getter;
    public GameObject bloodVFX;
    GameObject player;
    GameObject soundmanager;

    Sonido sound;
    private Camara myCam;

    public float axedamage;
    float damage;

    public bool candamage;
    public bool isaxe;
    public bool hasPlayedSound;

    public Attack ataque;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camara.instance;
        player = getter.Player;
        soundmanager = getter.Soundmanager;
        sound = soundmanager.GetComponent<Sonido>();   
        axedamage = 3;
        ataque = player.GetComponent<Attack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ataque.candamage == true )
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                myCam.ShakeCam();

                BasicEnemy enemigo = other.GetComponent<BasicEnemy>();

                #region Play Sound
                //this is so the sword only plays the sound of its first impact
                if (hasPlayedSound == false)
                {
                    if (other.GetComponent<BasicEnemy>().life - damage <= 0)
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
                if (isaxe == false)
                {
                    //sword deals more damage based on missing health
                    damage = 1 + (enemigo.maxlife - enemigo.life) / 1.5f;
              
                    enemigo.takedamage(damage, "Light", true);
                    enemigo.statuseffect("Iron");
                    bloodVFX.GetComponent<ParticleSystem>().Emit(100);
                }
                else
                {
                    damage = axedamage;
                    enemigo.takedamage(axedamage, "Heavy", true);
                    enemigo.statuseffect("Iron");
                    bloodVFX.GetComponent<ParticleSystem>().Emit(10000);
                }
                #endregion

                if (enemigo.life <= 0) ataque.ActivateSlowMo(0.2f, 0.08f);
            }
            else
            {
                if (hasPlayedSound == false)
                {
                    if (other.gameObject.tag.Equals("Damager"))
                    {
                        //This is in case the sword collides with the damagers in the enemy's hands
                        //In this case you get the value of life from the zombie (father) gameobject of the damager

                        myCam.ShakeCam();

                        BasicEnemy enemigo = other.GetComponent<Damager>().father.GetComponent<BasicEnemy>();
                        if (enemigo.life - damage <= 0)
                        {
                            sound.playaudio("Strong Sword Impact");

                        }
                        else
                        {
                            sound.playaudio("Sword Impact");
                        }
                        hasPlayedSound = true;
                        if(enemigo.life <= 0) ataque.ActivateSlowMo(0.2f, 0.08f);
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
