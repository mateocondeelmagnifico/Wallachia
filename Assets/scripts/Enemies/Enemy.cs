using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //THis Script controls the life and status effects of the enemy, it goes on all enemies

    public Animator animador;
    public Enemymovement othersript;
    public Image hitmarker;
    public Sonido sonido;
    public GameObject[] damager;

    public string enemytype;

    public float life;
    public float maxlife;
    public float transforming;
    public float stunresistance;
    public float regeneration;
    public float hitmarkertimer;
    float idletimer;
    float timer;

    public bool vulnerable;
    void Start()
    {
        idletimer = 4;
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
        //this is to enable the hitmarker when you hit an enemy
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

        //The more an enemy gets hit, the more stunresistance he builds up
        if (stunresistance > 0)
        {
            stunresistance -= Time.deltaTime * 0.25f;
        }

        checkdead();

        //this applies the transforming status effect, it affects zombies and werewolves differently
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

        //this is for idling sounds
        if (idletimer > 0)
        {
            idletimer -= Time.deltaTime;
        }
        else
        {
            choserandomsound();
        }
    }
    public void choserandomsound()
    {
        string Whichsound;
        int random = Random.Range(1, 4);
        Whichsound = random.ToString();
        sonido.playaudio("Idle " + Whichsound);
        idletimer = Random.Range(3, 6);
    }
    public void takedamage(float damage, string hitype)
    {
        sonido.playaudio("Hurt");
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
            othersript.navegador.velocity = new Vector3(0, 0, 0);
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
