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

    public bool candamage;
    public bool isaxe;

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
                sound.playaudio("Sword Impact");
                Enemy enemigo = other.GetComponent<Enemy>();
                if (isaxe == false)
                {
                    //sword deals more damage based on missing health
                    enemigo.takedamage(0.5f + (enemigo.maxlife - enemigo.life) / 4, "Light");
                    enemigo.statuseffect("Iron");
                    bloodVFX.GetComponent<ParticleSystem>().Emit(100);
                }
                else
                {
                    enemigo.takedamage(axedamage, "Heavy");
                    enemigo.statuseffect("Iron");
                    bloodVFX.GetComponent<ParticleSystem>().Emit(100);
                }
            }
            else
            {
                sound.playaudio("Sword impact wood");
            }
        }
    }
}
