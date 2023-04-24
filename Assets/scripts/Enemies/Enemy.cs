using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animador;
    public Enemymovement othersript;
    public Image hitmarker;
    public GameObject[] damager;

    public string enemytype;

    public float life;
    public float maxlife;
    public float transforming;
    public float stunresistance;
    public float regeneration;
    public float hitmarkertimer;
    float timer;

    public bool vulnerable;
    void Start()
    {

        if (enemytype == "Zombie")
        {
            maxlife = 6;
        }
        if(enemytype == "Werewolf")
        {
            maxlife = 10;
        }
        life = maxlife;
        othersript = GetComponent<Enemymovement>();
    }

    // Update is called once per frame
    void Update()  
    {

        if (hitmarker.enabled == false)
        {
            hitmarkertimer = 0;
        }
        if (hitmarkertimer > 0 && hitmarker.enabled == true)
        {
            hitmarkertimer -= Time.deltaTime;
            if (hitmarkertimer <= 0)
            {
                hitmarker.enabled = false;
            }
        }

        if (stunresistance > 0)
        {
            stunresistance -= Time.deltaTime * 0.25f;
        }

        checkdead();

        if (enemytype == "Zombie")
        {
            life -= transforming * Time.deltaTime;
        }
        if (enemytype == "Werewolf")
        {
            if (transforming > 0.2)
            {
                regeneration = 0.10f;
            }
            else
            {
                regeneration = 0.25f;
            }
            if (transforming < 0.4 && life < maxlife && life > 0)
            {
                life += Time.deltaTime * regeneration;
            }
        }
    }
    public void takedamage(float damage, string hitype)
    {
        if (life > 0)
        {
            life -= damage;
            stunresistance++;
            if (enemytype == "Werewolf" && stunresistance >3 && hitype == "Light")
            { 
                   
            }
            else
            {
                animador.SetBool("Hurt", true);
                othersript.isattacking = false;
            }
            othersript.playerdetected = true;
            othersript.angry = true;

            hitmarker.enabled = true;
            hitmarkertimer = 0.4f;

            statuseffect(hitype);
            decidestun(hitype);
        }
    }
    public void statuseffect(string type)
    {
        //Silver, Iron, Garlic and consecrated

        //Immune to iron
        if (type == "Garlic")
        {
            if (enemytype == "Zombie")
            {
                {
                    life -= 0.5f;
                }
            }
            if (enemytype == "Werewolf")
            {
                if (vulnerable == true)
                {
                    life -= 2;
                }
            }
        }
        if (type == "Silver")
        {
            transforming += 0.2f;
            if (enemytype == "Werewolf")
            {
                decidestun("Heavy");
                stunresistance++;
                stunresistance++;
            }
        }
        if (type == "Iron")
        {
            if (enemytype == "Zombie")
            {
                life -= 1f;
            }
        }
    }
    public void decidestun(string hitype)
    {
        float stunamount;
        if (life > 0)
        {
            //Decide stun duration based on if its a heavy or light attack
            if (hitype == "Light")
            {
                if (enemytype == "Zombie")
                {
                    stunamount = 2 - stunresistance;
                    if (stunamount > 0.4f)
                    {
                        othersript.staggered = stunamount;
                        othersript.attackposition = transform.position;
                    }
                }
            }
            if (hitype == "Heavy")
            {
                if (enemytype == "Zombie")
                {
                    stunamount = 3 - stunresistance / 2;
                    othersript.staggered = stunamount;
                }if (enemytype == "Werewolf")
                {
                    if (stunresistance < 3)
                    {
                        stunamount = 3 - stunresistance / 2;
                        if (stunamount > 0.4f)
                        {
                            othersript.staggered = stunamount;
                        }
                        vulnerable = true;
                    }
                }
                othersript.attackposition = transform.position;
            }
        }
    }
    public void checkdead()
    {
        if (life <= 0)
        {
            if (enemytype == "Werewolf")
            {
                GetComponent<Werewolf>().myball.GetComponent<circlefollow>().istaken = false;
                GetComponent<Werewolf>().myball.SetActive(false);
            }
            damager[0].SetActive(false);
            damager[1].SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
            othersript.navegador.isStopped = true;
            othersript.enabled = false;
            animador.SetBool("Dead", true);
        }
    }
}
