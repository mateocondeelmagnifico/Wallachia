using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMechanics;

public class WereWolf : Enemy
{
    public Transform playerPointer;
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

        if (hitType == "Heavy")
        {
            stun = 0.8f;
        }
        #endregion

        DecideStun(stun);
    }

    public override void StatusEffect(string type)
    {
        if (type == "Garlic")
        {
           life -= 2f;
        }

        if (type == "Silver")
        {
            if(regeneration > -1.7f) regeneration -= 0.6f;
            DecideStun(1.4f);
        }
    }

    protected override void EmptyUpdate()
    {

        if(playerDetected && !angry && Vector3.Distance(player.transform.position, transform.position) < 17)
        {
            playerPointer.forward = player.transform.position - transform.position;

            destination = transform.position + (playerPointer.right.normalized * 2);
        }
    }

    protected override void DyingEffects()
    {
        scaryness.IncreaseScaryness(0.5f);
    }
}
