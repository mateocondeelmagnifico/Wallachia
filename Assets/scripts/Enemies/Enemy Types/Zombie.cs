using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMechanics;

public class Zombie : Enemy
{
    public override void TakeDamage(float damage, string hitType, bool playSound, float staggerAmount)
    {
        stagger += staggerAmount;

        if (playSound)
        {
            sonido.playaudio(enemytype + "Hurt", sources[1]);
        }

        isattacking = false;

        ChangeLife(damage);
        StatusEffect(hitType);

        #region Calculate Stun
        float stun = 0;
        if(hitType == "Light")
        {
            stun = 0.25f + damage/8;
        }
        if (hitType == "Heavy")
        {
            stun = 0.5f + damage / 6;
        }
        if(hitType == "Weakness")
        {
            stun = 2;
        }
        #endregion

        DecideStun(stun);
    }

    public override void StatusEffect(string type)
    {

        if (invulnerable != true)
        {
            if (type == "Garlic")
            {
                life -= 3f;
            }
            if (type == "Silver")
            {
                regeneration -= 0.2f;
            }
        }

        if (type == "Holy" && damageTimer <= 0)
        {
            TakeDamage(0.7f, "Weakness", false, 1);
            damageTimer = 0.2f;
        }
    }
    protected override void EmptyUpdate()
    {
        if (regeneration < 0 && isplaying == false)
        {
            particles.Play();
            isplaying = true;
        }

        if (scaryness.howScary > 1.5f)
        {
            //moverse para atrás
            if (scaryness.howScary > 2.5f)
            {
                transform.LookAt(player.transform.position);

                if (Vector3.Distance(player.transform.position, transform.position) < 15)
                {
                    Vector3 direction = transform.position -player.transform.position;
                    destination = transform.position + (direction.normalized * 2);
                }
                else
                {
                    destination = transform.position;
                    navegador.isStopped = true;
                }
            }
        }
    }

    protected override void SpeedChanges()
    {
        //Baja la velocidad si tiene miedo o por el efecto de la plata

        if (scaryness.howScary > 1.5f) speed /= 2;
        
        if (scaryness.howScary > 2.5f) speed /= 2;

        if (regeneration <0) speed -= Mathf.Abs(Mathf.Clamp(regeneration,0.1f,1));
    }
}
