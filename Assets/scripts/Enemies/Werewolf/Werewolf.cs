using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Werewolf : MonoBehaviour
{
    Enemymovement movimiento;
    Animator animador;

    Transform player;
    public GameObject groupmanager;
    public GameObject myball;

    Groupmanager manager;
    NavMeshAgent navegador;


    public bool wandering;
    public bool alert;
    public bool encircling;
    public bool attacking;
    public bool circleinplace;

    public float wandertimer;
    public float attacktimer;
    float speed;
    void Start()
    {
        navegador = GetComponent<NavMeshAgent>();
        speed = navegador.speed;
        manager = groupmanager.GetComponent<Groupmanager>();
        attacktimer = Random.Range(6, 10);
        player = manager.player.transform;
        movimiento = GetComponent<Enemymovement>();
        animador = GetComponent<Animator>();
        movimiento.attackingrange = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 15 || movimiento.playerdetected == true)
        {
            alert = true;
        }

        if (Vector3.Distance(player.position, transform.position) < 14 && attacking == false || encircling == true && attacking == false)
        {
            //se desactiva el movimiento
            encircle();
        }

        if (encircling == true && manager.isattacking == false)
        {
            attacktimer -= Time.deltaTime;
            if (attacktimer <= 0)
            {
                attacking = true;
                manager.isattacking = true;
            }
        }

        Attackcheck();

        alertcheck();

        wandertimer -= Time.deltaTime;
        if (alert == false)
        {
            wander();
        }
        if (GetComponent<Enemy>().life <= 0)
        {
            if (circleinplace == false)
            {
                GameObject.Find("Ball manager").GetComponent<Ballmanger>().assingball(this.gameObject);
                circleinplace = true;
            }
            if (attacking == true && manager.isattacking == true)
            {
                manager.isattacking = false;
            }
        }

        if (movimiento.isattacking == true)
        {
            if (movimiento.staggered <= 0)
            {
                speed = 6;
            }
        }

        if (movimiento.staggered > 0)
        {
            speed = 0;
        }
        else
        {
            if (speed < 6)
            {
                speed++;
            }
        }
        if(encircling == true && attacking == false)
        {
            if (myball.GetComponent<circlefollow>().speed == 25)
            {
                speed = 15;
            }
        }
        navegador.speed = speed;
    }
    public void wander()
    {
        if (wandertimer <= 0)
        {
            int decidestomove = Random.Range(0, 6);
            if (decidestomove >= 3)
            {
                //move
                movimiento.destination = new Vector3(Random.Range(-8, 7), transform.position.y, Random.Range(-8, 7));
                wandertimer = Random.Range(2.5f, 4);
                animador.SetBool("Moving", true);
            }
            else
            {
                //don't move
                wandertimer = Random.Range(2.5f, 4);
                animador.SetBool("Moving", false);
            }
        }
    }

    public void Attackcheck()
    {
        if (manager.isattacking == true)
        {
            if (attacking == false)
            {
                attacktimer = Random.Range(6, 10);
            }
        }
        if (movimiento.angry == true)
        {
            attacking = true;
        }

        if (attacking == true)
        {
            movimiento.enabled = true;
            if (speed < 8 && movimiento.staggered <= 0)
            {
                speed += Time.deltaTime;
            }
            movimiento.destination = player.position;
        }
    }
    public void encircle()
    {
        animador.SetBool("Moving", true);
        if (circleinplace == false)
        {
            GameObject.Find("Ball manager").GetComponent<Ballmanger>().assingball(this.gameObject);
            myball.transform.position = this.transform.position;
            circleinplace = true;
        }
        movimiento.playerdetected = false;

        encircling = true;
        movimiento.destination = myball.transform.position;
    }

    public void alertcheck()
    {
        if (alert == true && encircling == false)
        {
            movimiento.playerdetected = false;
            manager.activated = true;
        }

        if (manager.activated == true && encircling == false)
        {
            alert = true;
            movimiento.playerdetected = true;
        }
    }
}
