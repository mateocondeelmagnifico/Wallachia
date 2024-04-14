using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMechanics;

public class WereWolf : Enemy
{
    public Transform playerPointer;
    private float timer;
    private bool isScared;
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
            stun = damage/8 - 0.2f;
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
            DecideStun(1f);
        }
    }

    protected override void EmptyUpdate()
    {
        if(playerDetected && !angry && Vector3.Distance(player.transform.position, transform.position) < 17)
        {
            //Surround player
            playerPointer.forward = player.transform.position - transform.position;

            destination = transform.position + (playerPointer.right.normalized * 2);
        }

        #region Check Scared
        if (scaryness.howScary > 1.5f) speed /= 3;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (isScared)
        {
            isScared = false;
            isattacking = true;
            angry = true;
        }
        #endregion
    }

    protected override void DyingEffects()
    {
        scaryness.IncreaseScaryness(0.5f);
    }

    public void SendToPlayer()
    {
        if(scaryness.howScary < 2.5f)
        {
            isattacking = true;
            angry = true;
        }
        else
        {
            timer = 2;
            isScared = true;
        }
    }
}
