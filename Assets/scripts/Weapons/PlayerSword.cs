using EnemyMechanics;
using UnityEngine;
using WeaponMechanics;

public class PlayerSword : Sword
{
    [SerializeField] private Light[] myLight;
    public float damageTimer;
    private float[] myIntesities;

    private void Start()
    {
        myIntesities = new float[myLight.Length];

        for(int i = 0; i < myLight.Length; i++)
        {
            myIntesities[i] = myLight[i].intensity;
        }
    }

    private void Update()
    {
        //Sword charges passively
        if (damageTimer < 4)
        {
            damageTimer += Time.deltaTime;
        }

        //Since each light has different max intensities this code is neccesary
        for (int i = 0; i < myLight.Length; i++)
        {
            myLight[i].intensity = damageTimer * myIntesities[i] / 4;
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

        for (int i = 0; i < myLight.Length; i++)
        {
            myLight[i].intensity = 0;
        }
    }
}
