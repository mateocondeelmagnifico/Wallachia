using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfEnemy : BasicEnemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        animador = GetComponent<Animator>();
        sonido = GetComponent<Sonido>();
        particles = GetComponent<ParticleSystem>();
        idletimer = 4;
        maxlife = 5;
        life = maxlife;
        othersript = GetComponent<WerewolfMovement>();
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


        checkdead();

        #region applyTransforming
        //this applies the transforming status effect, it affects zombies and werewolves differently

        if (transforming > 0.2)
        {
            regeneration = 0.30f;
            if (isplaying == false)
            {
                particles.Play();
                isplaying = true;
            }
        }
        else
        {
            regeneration = 0.7f;
        }
        if (transforming < 0.4 && life < maxlife && life > 0)
        {
            life += Time.deltaTime * regeneration;
        }

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
        sonido.playaudio("Hurt");
        if (life > 0)
        {
            SetHitmarker(damage);


            if (hitype == "Heavy")
            {
                animador.SetBool("Hurt", true);
                othersript.isattacking = false;
            }
            life -= damage;

            othersript.playerdetected = true;
            othersript.angry = true;

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
            if (type == "Silver")
            {
                transforming += 0.2f;
                decidestun("Heavy");
            }
        }
    }

    public override void decidestun(string hitype)
    {
        float stunamount;
        if (life > 0)
        {
            //Decide stun duration based on if its a heavy or light attack
            if (hitype == "Heavy")
            {

                    stunamount = 1.5f;
                    if (stunamount > 0.4f)
                    {
                        othersript.staggered = stunamount;
                    }
                    vulnerable = true;
                }
                othersript.attackposition = transform.position;
            
        }
    }
}
