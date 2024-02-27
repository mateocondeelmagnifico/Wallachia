using UnityEngine;
using UnityEngine.AI;
using EnemyMechanics;

public class Werewolf : MonoBehaviour
{
    //This is for the behaviours of the werewolf (Wandering, alert, attacking or encircling)
    //They are a lot more complicated than zombies
    // When they come close to a player they start encircling him and attack one at a time
    //The speed of the werewolf is also modified

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
            //When they are encricling, instead of following the player they each follow a ball that rotates arounf the player
            //they each have their own ball
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
                //This is for claiming a ball to follow when they're encircling the player
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
                movimiento.destination = new Vector3(transform.position.x + Random.Range(-8, 7), transform.position.y,transform.position.z + Random.Range(-8, 7));
                wandertimer = Random.Range(2.5f, 4);
            }
            else
            {
                //don't move
                wandertimer = Random.Range(2.5f, 4);
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

    public void returnHome(Vector3 destination)
    {
        //This script is so the enemy doesn't stray from their destined location
        //It is accesed by the groupmanager
        wandertimer = 4;
        movimiento.destination = destination;
    }
}
