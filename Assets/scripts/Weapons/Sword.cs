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

    public float axedamage;
    float damage;

    public bool candamage;
    public bool isaxe;
    public bool hasPlayedSound;

    public Attack ataque;
    // Start is called before the first frame update
    void Start()
    {
        player = getter.Player;
        soundmanager = getter.Soundmanager;
        sound = soundmanager.GetComponent<Sonido>();   
        axedamage = 3;
        ataque = player.GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ataque.candamage == true )
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                
                Enemy enemigo = other.GetComponent<Enemy>();

                if (isaxe == false)
                {
                    //sword deals more damage based on missing health
                    damage = 0.5f + (enemigo.maxlife - enemigo.life) / 4;
                    

                    enemigo.takedamage(damage, "Light");
                    enemigo.statuseffect("Iron");
                    bloodVFX.GetComponent<ParticleSystem>().Emit(100);

                   
                }
                else
                {
                    damage = axedamage;
                    enemigo.takedamage(axedamage, "Heavy");
                    enemigo.statuseffect("Iron");
                    bloodVFX.GetComponent<ParticleSystem>().Emit(100);
                }
                //this is so the sword only plays the sound of its first impact
                if (hasPlayedSound == false)
                {
                    if (other.GetComponent<Enemy>().life - damage <= 0)
                    {
                        sound.playaudio("Strong Sword Impact");

                    }
                    else
                    {
                        sound.playaudio("Sword Impact");
                    }
                    hasPlayedSound = true;
                }
            }
            else
            {
                if (hasPlayedSound == false)
                {
                    if (other.gameObject.tag.Equals("Damager"))
                    {
                        //This is in case the sword collides with the damagers in the enemy's hands
                        //In this case you get the value of life from the zombie (father) gameobject of the damager
                        if (other.GetComponent<Damager>().father.GetComponent<Enemy>().life - damage <= 0)
                        {
                            sound.playaudio("Strong Sword Impact");

                        }
                        else
                        {
                            sound.playaudio("Sword Impact");
                        }
                        hasPlayedSound = true;
                    }
                    else
                    {
                        sound.playaudio("Sword impact wood");
                    }
                    hasPlayedSound = true;
                }
            }
        }
    }
}
