using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class ZombieEnemy : BasicEnemy
{

    public override void Start()
    {
        animador = GetComponent<Animator>();
        sonido = GetComponent<Sonido>();
        particles = GetComponent<ParticleSystem>();
        idletimer = 4;
        maxlife = 6;
        life = maxlife;
        othersript = GetComponent<BasicEnemyMovement>();
    }

    // Update is called once per frame
    public override void Update()
    {
        #region enable hitmarker
        //this is to enable the hitmarker when you hit an enemy
        if (hitmarker.enabled == false)
        {
            hitmarkertimer = 0;
        }
        if (hitmarkertimer > 0 && hitmarker.enabled == true)
        {
            hitmarkertimer -= Time.deltaTime;
            if (hitmarkertimer <= 0)
            {
                hitmarker.enabled = false;
            }
        }
        #endregion

        if (life <= maxlife / 4)
        {
            invulnerable = true;
        }

        if (vulnerableTimer > 0)
        {
            vulnerableTimer -= Time.deltaTime;
        }
        else
        {
            vulnerable = false;
        }
        if(damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }

        //The more an enemy gets hit, the more stunresistance he builds up

        checkdead();

        #region applyTransforming
        //this applies the transforming status effect, it affects zombies and werewolves differently
     
        life -= transforming * Time.deltaTime;
        if (transforming > 0 && isplaying == false)
        {
          particles.Play();
          isplaying = true;
        }
        #endregion

        #region idilingSounds
        //this is for idling sounds
        if (idletimer > 0)
        {
            idletimer -= Time.deltaTime;
        }
        else
        {
            choserandomsound();
        }
        #endregion
    }
    public override void takedamage(float damage, string hitype, bool playSound)
    {
    
        if(playSound)
        {
            sonido.playaudio("Hurt");
        }

        if (life > 0)
        {
            SetHitmarker(damage);
            //This breaks invulnerability for zombie enemies
            if (damage >= maxlife / 4)
            {
                invulnerable = false;
            }

            if (invulnerable == false)
            {
                life -= damage;
            }
            else
            {
                life -= damage / 2;
            }

            othersript.isattacking = false;
           
            othersript.playerdetected = true;

            statuseffect(hitype);
            decidestun(hitype);
        }
    }

    public override void statuseffect(string type)
    {
        //Silver, Iron, Garlic and consecrated

        //Currently iron does nothing

        if (invulnerable != true)
        {
            if (type == "Garlic")
            {
                life -= 3f;
            }
            if (type == "Silver")
            {
                transforming += 0.2f;
            }
        }

        if (type == "Holy" && damageTimer <= 0)
        {
            takedamage(0.1f, "Weakness", false);
            damageTimer = 0.2f;
        }
    }

    public override void decidestun(string hitype)
    {
        float stunamount;
        if (life > 0)
        {
            //Decide stun duration based on if its a heavy or light attack
            if (hitype == "Light")
            {
                stunamount = 0.5f;
                if (stunamount > 0.4f)
                {
                   othersript.staggered = stunamount;
                   othersript.attackposition = transform.position;
                }

                animador.SetBool("Hurt", true);
            }
            if (hitype == "Heavy")
            {
                stunamount = 1.3f;
                othersript.staggered = stunamount;
                
                othersript.attackposition = transform.position;

                animador.SetBool("Hurt", true);
            }
            if (hitype == "Weakness" && vulnerableTimer <= 0)
            {
                stunamount = 3;
                othersript.staggered = stunamount;
                vulnerableTimer = 3;
                vulnerable = true;

                sonido.playaudio("Hurt");
                animador.SetBool("Hurt", true);
            }
        }
    }
}
