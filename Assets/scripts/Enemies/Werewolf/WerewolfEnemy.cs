using PlayerMechanics;
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
        maxlife = 10;
        life = maxlife;
        othersript = GetComponent<WerewolfMovement>();
        setUIPlayer = SetUiValues.Instance;
    }

    // Update is called once per frame
    public override void Update()
    {
        checkdead();

        if(stunTimer <= 0)
        {
            stunResistance = 0;
        }
        else
        {
            stunTimer -= Time.deltaTime;
        }

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
            regeneration = 1f;
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
            othersript.attacking = true;

            statuseffect(hitype);
            decidestun(hitype);
        }

        if (life <= 0 && othersript.attacking)
        {
            othersript.groupManager.isattacking = false;
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
                    stunamount = 1.5f - stunResistance;
                    if (stunamount > 0.4f)
                    {
                        othersript.staggered = stunamount;
                    }
                    vulnerable = true;

                if (!animador.GetBool("Hurt"))
                {
                    animador.SetBool("Hurt", true);
                }
            }
                othersript.attackposition = transform.position;
            
        }
        stunResistance += 0.20f;
        if (stunResistance > 1)
        {
            stunResistance = 1; 
        }
        stunTimer = 4;
    }
}
