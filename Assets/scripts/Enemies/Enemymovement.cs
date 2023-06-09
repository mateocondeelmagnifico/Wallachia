using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemymovement : MonoBehaviour
{
    //This script takes care of enemy movement, movement animations and speed
    //It also makes the enemy attack if it detects it's too close to the player
    //This script goes on all enemies

    public NavMeshAgent navegador;
    public Animator animador;
    public Sonido sonido;

    public GameObject player;

    public bool playerdetected;
    public bool isattacking;
    public bool angry;
    public bool candamage;
    public bool iswerewolf;
    public bool isinholy;
    bool isdamaging;
    bool hasreached;
    bool isinplace;

    public float staggered;
    //must be modified from enemy scripts
    public float attackingrange;
    float lungespeed;
    
    public Vector3 destination;
    public Vector3 attackposition;
    void Start()
    {
        destination = this.transform.position;
        animador = GetComponent<Animator>();
        navegador = GetComponent<NavMeshAgent>();
        if (iswerewolf == true)
        {
            lungespeed= 7;
        }
        else
        {
            lungespeed = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (playerdetected == true)
        {

            destination = player.transform.position;
        }

        if (navegador.velocity != Vector3.zero)
        {
            animador.SetBool("Moving", true);
        }
        else
        {
            animador.SetBool("Moving", false);
        }

        navegador.SetDestination(destination);

        if (transform.position == destination && playerdetected == false)
        {
            //animador.SetBool("Moving", false);
        }

        //Attack
        checkdistance();
        checkstun();
        checkattack();

    }
    //If the player is too close, it attacks
    public void checkdistance()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attackingrange && isattacking == false && staggered <= 0)
        {
            attackposition = transform.position;
            animador.SetTrigger("Attack");
            navegador.isStopped = true;
            isattacking = true;
        }
    }
    public void checkstun()
    {
        if (staggered > 0)
        {
            //transform.position = attackposition;
            staggered -= Time.deltaTime;
            navegador.isStopped = true;
            animador.SetBool("Moving", false);
            candamage = false;
            navegador.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            animador.SetBool("Hurt", false);
            if (isattacking == false)
            {
                navegador.isStopped = false;
            }
        }
    }

    //These three scripts are called by animation events
    public void Damagestart()
    {
        sonido.playaudio("Attack");
        candamage = true;
        isdamaging = true;
    }
    public void Endattack()
    {
        isattacking = false;
        isinplace = false;
    }
    public void Enddamage()
    {
        candamage = false;
        isdamaging = false;
    }
 
    //This is so enemies don't drift when they start to attack, it also makes it so that they lunge at the player when the attack starts
    void checkattack()
    {
        if (isattacking == true)
        {
            transform.LookAt(player.transform.position);
            if (candamage == false && isdamaging == true)
            {
                navegador.velocity = new Vector3(0, 0, 0);
            }
            navegador.isStopped = true;
            if (isinplace == false)
            {
                transform.position = attackposition;
                if (candamage == true)
                {
                    isinplace = true;
                }
            }
        }
        else
        {
            if (iswerewolf == true)
            {
                navegador.isStopped = false;
            }
        }

        if (candamage == true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 1 && staggered <= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, lungespeed * Time.deltaTime);
                hasreached = false;
            }
            else
            {
                if (hasreached == false)
                {
                    attackposition = transform.position;
                    hasreached = true;
                }

                transform.position = attackposition;
            }
        }
    }
}
