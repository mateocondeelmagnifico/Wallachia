using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newsilverimpact : MonoBehaviour
{
    //Grenade that leaves a continous poison zone for enemies, also slows them down
    //Emits smoke VFX

    float timer;
    float hurttimer;
    ParticleSystem particles;
    void Start()
    {
        particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        timer = 2;
        hurttimer = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            particles.Emit(100);
        }
        
        
        if (timer <= -3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Enemy") && timer > 0)
        {
            other.GetComponent<Enemy>().takedamage(0.2f, "Light");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("Enemy") && timer > 0)
        {
            hurttimer -= Time.deltaTime;
            if (hurttimer <= 0)
            {
                other.GetComponent<Enemy>().statuseffect("Silver");
                hurttimer = 0.3f;
            }
        }
    }
}
