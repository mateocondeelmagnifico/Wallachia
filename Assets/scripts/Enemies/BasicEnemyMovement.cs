using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class BasicEnemyMovement : MonoBehaviour
{
    //This script takes care of enemy movement, movement animations and speed
    //It also makes the enemy attack if it detects it's too close to the player
    //This script goes on all enemies

    public NavMeshAgent navegador;
    public Animator animador;
    public Groupmanager groupManager;
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
    bool alert;

    public float staggered;
    float wanderTimer;
    public float speed;
    //must be modified from enemy scripts
    public float attackingrange;
    public float lungespeed;

    public Vector3 destination;
    public Vector3 attackposition;
    public virtual void Start()
    {
        sonido = GetComponent<Sonido>();
        destination = this.transform.position;
        animador = GetComponent<Animator>();
        navegador = GetComponent<NavMeshAgent>();
        SetStartValues();
    }

    // Update is called once per frame
    void Update()
    {
        #region Player Detection
        if (groupManager.activated == true)
        {
            alert = true;
        }

        if (playerdetected == true)
        {
            destination = player.transform.position;
        }
        #endregion

        #region Set moving Animation
        if (navegador.velocity != Vector3.zero)
        {
            animador.SetBool("Moving", true);
        }
        else
        {
            animador.SetBool("Moving", false);
        }
        #endregion

        VoidFunction();

        ModifySpeed();

        wanderTimer -= Time.deltaTime;

        navegador.speed = speed;
        navegador.SetDestination(destination);

        //Attack
        checkdistance();
        checkstun();
        checkattack();
        DetectPlayer();
        if(!alert)
        {
            wander();
        }
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

    #region Damage Steps Functions
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
    #endregion

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
    void wander()
    {
        if (wanderTimer <= 0)
        {
            int decidestomove = Random.Range(0, 6);
            if (decidestomove >= 3)
            {
                //move
                destination = new Vector3(transform.position.x + Random.Range(-6, 5), transform.position.y, transform.position.z + Random.Range(-6, 5));
                wanderTimer = Random.Range(3, 5);
            }
            else
            {
                //don't move
                wanderTimer = Random.Range(3, 5);
            }
        }
    }
    void DetectPlayer()
    {
        //Detect player
        if (Vector3.Distance(player.transform.position, transform.position) < 8 || playerdetected == true)
        {
            alert = true;
        }
        if (alert == true)
        {
 
           if (speed < 6)
           {
             speed += Time.deltaTime;
           }
            
           playerdetected = true;
           groupManager.activated = true;
        }
    }

    public virtual void VoidFunction()
    {
        //This function is made for monster who need to access update so that it doesn't have to be overrided
    }

    public virtual void ModifySpeed()
    {
        if (alert)
        {
            speed = 5;
        }
        if (staggered > 0 || isattacking == true)
        {
            speed = 3;
        }
        if (isinholy == true)
        {
            speed /= 2;
        }
    }
    public virtual void SetStartValues()
    {
        speed = 2;
        attackingrange = 2;
        if (iswerewolf == true)
        {
            lungespeed = 7;
        }
        else
        {
            lungespeed = 6;
        }
    }
    public void returnHome(Vector3 Destination)
    {
        //This script is so the enemy doesn't stray from their destined location
        //It is accesed by the groupmanager
        wanderTimer = 4;
        destination = Destination;
    }
}
