using EnemyMechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponMechanics;

public class Axe : Sword
{
    protected override void DealDamage(Enemy enemigo)
    {
        enemigo.TakeDamage(damage + (scaryness.howScary / 8), "Heavy", true, Mathf.Clamp(damage, 1.5f, 3));
        bloodVFX.GetComponent<ParticleSystem>().Emit(10000);
    }
}
