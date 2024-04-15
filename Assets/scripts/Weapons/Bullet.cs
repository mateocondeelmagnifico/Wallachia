using UnityEngine;
using EnemyMechanics;

namespace WeaponMechanics
{
    public class Bullet : MonoBehaviour
    {
        private Enemy enemigo;

        public Rigidbody cuerporigido;
        public GameObject[] particles;
        public Transform player;
        GameObject myparticle;

        public float timer, damage;
        private int myType;

        public weapons armas;
        public bool isriflebullet;
        bool issilver;
        void Start()
        {
            timer = 2.5f;
            cuerporigido = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            cuerporigido.velocity = transform.forward * 120;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.tag.Equals("Enemy"))
            {
                enemigo = collision.gameObject.GetComponent<Enemy>();
                hitenemy(true);
            }
            else
            {
                hitenemy(false);
            }
            Destroy(this.gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Damager") || other.gameObject.tag.Equals("Hitbox"))
            {
                string stun;
                string statuseffect = "";
                if (isriflebullet == true)
                {
                    stun = "Heavy";
                }
                else
                {
                    stun = "Light";
                }

                switch(myType)
                {
                    case 0:
                        statuseffect = "Iron";
                        break;
                    case 1:
                        statuseffect = "Silver";
                        break;
                    case 2:
                        statuseffect = "Holy";
                        break;
                }

                other.GetComponent<Hitbox>().DealDamage(damage, stun, statuseffect);
                Destroy(this.gameObject);
            }
        }

        public void hitenemy(bool hashit)
        {
            //if the bullet is a silver one
            if (armas.currentEquip[3] == 1)
            {
                issilver = true;
                damage = 1f;
            }
            else
            {
                damage = 2;
            }

            if (hashit == true)
            {
                damage -= Vector3.Distance(transform.position, player.position) / 9;
                if (damage < 0.7f)
                {
                    damage = 0.7f;
                }
                myparticle = GameObject.Instantiate(particles[0], transform.position, transform.rotation);
                myparticle.GetComponent<BloodVFX>().orientation = -transform.forward;
                if (isriflebullet == true)
                {
                    enemigo.TakeDamage(damage * 3.5f, "Heavy", true);
                }
                else
                {
                    enemigo.TakeDamage(damage, "Light", true);
                }

                string status = "";

                switch (myType)
                {
                    case 0:
                        status = "Iron";
                        break;
                    case 1:
                        status = "Silver";
                        break;
                    case 2:
                        status = "Holy";
                        break;
                }

                enemigo.StatusEffect(status);
                if (isriflebullet == true)
                {
                   enemigo.StatusEffect(status);
                }

            }
            else
            {
                myparticle = GameObject.Instantiate(particles[1], transform.position, transform.rotation);
                myparticle.GetComponent<BloodVFX>().orientation = -transform.forward;
            }
        }

        public void GiveType(int type)
        {
            myType = type;
        }
    }
}
