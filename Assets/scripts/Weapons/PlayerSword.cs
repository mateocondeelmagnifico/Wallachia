using EnemyMechanics;
using UnityEngine;
using WeaponMechanics;

public class PlayerSword : Sword
{
    private float damageTimer;

    private void Update()
    {
        //Sword charges passively
        if(damageTimer < 4 && !candamage)
        {
            damageTimer += damageTimer;
        }
    }

    protected override void DealDamage(Enemy enemigo) 
    {
        //sword deals more damage if fully charged

        float extraDmg = 0;
        if (damageTimer >= 4)
        {
            extraDmg = 3;
            damageTimer = 0;
        }

        damage = 1 + extraDmg + scaryness.howScary / 8;

        enemigo.TakeDamage(damage, "Light", true, 2 + extraDmg);
        bloodVFX.GetComponent<ParticleSystem>().Emit(100);

        if(damageTimer < 4) myCam.ShakeCam(1);
        else myCam.ShakeCam(2);
    }
}
