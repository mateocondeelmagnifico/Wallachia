using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    //Estados de la IA
    public Enemymovement movimiento;

    public Transform player;
    public GameObject groupmanager;
    NavMeshAgent navegador;

    public float wandertimer;
    public float speed;

    public bool Wandering;
    public bool alert;
    public bool frenzy;

    Animator animador;
    void Start()
    {
        navegador = GetComponent<NavMeshAgent>();
        speed = navegador.speed;
        wandertimer = 3;
        movimiento = GetComponent<Enemymovement>();
        animador = GetComponent<Animator>();
        movimiento.attackingrange = 1.5f;
        player = groupmanager.GetComponent<Groupmanager>().player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 8 || movimiento.playerdetected == true)
        {
            alert = true;
        }
        if (alert == true)
        {
            if (movimiento.angry == true)
            {
                if (speed < 6.5f)
                {
                    speed += Time.deltaTime;
                }
            }
            else
            {
                if (speed < 6)
                {
                    speed += Time.deltaTime;
                }
            }
            movimiento.playerdetected = true;
            groupmanager.gameObject.GetComponent<Groupmanager>().activated = true;
        }
        //velocity check
        if (movimiento.staggered > 0 || movimiento.isattacking == true)
        {
            speed = 3;
        }

        if (movimiento.isinholy == false)
        {
            navegador.speed = speed;
        }
        else
        {
            navegador.speed = speed / 2;
        }

        if (groupmanager.gameObject.GetComponent<Groupmanager>().activated == true)
        {
            alert = true;
            movimiento.playerdetected = true;
        }
        
        wandertimer -= Time.deltaTime;
        if (alert == false)
        {
            wander();
        }
    }
    public void wander()
    {
        if (wandertimer <=0)
        {
            int decidestomove = Random.Range(0, 6);
            if (decidestomove >= 3)
            {
                //move
                movimiento.destination = new Vector3( transform.position.x + Random.Range(-6, 5), transform.position.y, transform.position.z + Random.Range(-6, 5));
                wandertimer = Random.Range(3,5);
                animador.SetBool("Moving", true);
            }
            else
            {
                //don't move
                wandertimer = Random.Range(3, 5);
                animador.SetBool("Moving", false);
            }
        }
       
    }
  
}
