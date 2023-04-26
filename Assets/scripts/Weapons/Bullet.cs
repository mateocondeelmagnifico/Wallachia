using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Enemy enemigo;

    public Rigidbody cuerporigido;
    public GameObject[] particles;
    GameObject myparticle;

    public float timer;
    public float damage;

    public weapons armas;
    public bool isriflebullet;
    bool issilver;
    void Start()
    {
        timer = 2.5f;
        cuerporigido = GetComponent<Rigidbody>();
        if (armas.currentbullet == "Silver")
        {
            issilver = true;
            damage = 0.5f;
        }
        else
        {
            damage = 1;
        }
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
        if (other.gameObject.tag.Equals("Damager"))
        {
            string stun;
            string statuseffect;
            if (isriflebullet == true)
            {
                stun = "Heavy";
            }
            else
            {
                stun = "Light";
            }
            if (issilver == true)
            {
                statuseffect = "Silver";
            }
            else
            {
                statuseffect = "Iron";
            }
            other.GetComponent<Damager>().dealdamage(damage, stun, statuseffect);
        }
        Destroy(this.gameObject);
    }

    public void hitenemy(bool hashit)
    {
        if (hashit == true)
        {
            myparticle = GameObject.Instantiate(particles[0], transform.position, transform.rotation);
            myparticle.GetComponent<BloodVFX>().orientation = -transform.forward;
            if (isriflebullet == true)
            {
                enemigo.takedamage(damage * 3.5f, "Heavy");
            }
            else
            {
                enemigo.takedamage(damage, "Light");
            }

            if (issilver == true)
            {
                enemigo.statuseffect("Silver");
                if (isriflebullet == true)
                {
                    enemigo.statuseffect("Silver");
                }
            }
            else
            {
                enemigo.statuseffect("Iron");
            }

        }
        else
        {
            myparticle = GameObject.Instantiate(particles[1], transform.position, transform.rotation);
            myparticle.GetComponent<BloodVFX>().orientation = -transform.forward;
        }
    }
}
