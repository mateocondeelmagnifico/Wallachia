using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMechanics;

public class Zombie : Enemy
{
    public override void TakeDamage(float damage, string hitType, bool playSound)
    {
        if (playSound)
        {
            sonido.playaudio("Hurt");
        }

        isattacking = false;

        ChangeLife(damage);
        StatusEffect(hitType);

        #region Calculate Stun
        float stun = 0;
        if(hitType == "Light")
        {
            stun = 1;
        }
        if (hitType == "Heavy")
        {
            stun = 1.5f;
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
            TakeDamage(0.1f, "Weakness", false);
            damageTimer = 0.2f;
        }
    }
}
