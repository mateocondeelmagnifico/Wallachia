using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemymovement : MonoBehaviour
{
    public NavMeshAgent navegador;
    public Animator animador;
    Rigidbody cuerporigido;

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
        cuerporigido = GetComponent<Rigidbody>();
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
            if ( staggered <= 0)
            {
                animador.SetBool("Moving", true);
            }

            destination = player.transform.position;
        }

        navegador.SetDestination(destination);

        if (transform.position == destination && playerdetected == false)
        {
            animador.SetBool("Moving", false);
        }

        //Attack
        checkdistance();
        checkstun();
        checkattack();
        
    }
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
    public void Damagestart()
    {
        candamage = true;
        isdamaging = true;
    }
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
