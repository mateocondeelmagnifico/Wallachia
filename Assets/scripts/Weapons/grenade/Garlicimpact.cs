using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMechanics;

namespace WeaponMechanics
{
    public class Garlicimpact : MonoBehaviour
    {
        //Regular grenade explosion
        //It also emits explosion VFX

        public float timer;
        ParticleSystem particles;
        void Start()
        {
            Camara.instance.ShakeCam(3);
            particles = transform.GetChild(0).GetComponent<ParticleSystem>();
            timer = 0.75f;
        }

        // Update is called once per frame
        void Update()
        {
            if (timer > 0.6)
            {
                particles.Play();
            }
            timer -= Time.deltaTime;
            if (timer <= -3)
            {
                Destroy(this.gameObject);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Enemy") && timer > 0)
            {
                other.GetComponent<Enemy>().TakeDamage(1.5f, "Light", true, 1);
                other.gameObject.GetComponent<Enemy>().StatusEffect("Garlic");
            }
        }
    }
}
