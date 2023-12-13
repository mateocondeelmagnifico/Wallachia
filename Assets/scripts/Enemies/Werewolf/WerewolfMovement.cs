using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfMovement : BasicEnemyMovement
{
    public GameObject myBall;

    public bool circleInPlace;
    bool encircling;

    float attackTimer;

    public override void SetStartValues()
    {
        speed = 4;
        attackingrange = 3;
        lungespeed = 8;
    }

    public override void VoidFunction()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 14 && attacking == false || encircling == true && attacking == false)
        {
            //When they are encricling, instead of following the player they each follow a ball that rotates arounf the player
            //they each have their own ball
            encircle();
        }

        if (encircling == true && groupManager.isattacking == false)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attacking = true;
                groupManager.isattacking = true;
            }
        }
    }
    public void encircle()
    {
        if (circleInPlace == false)
        {
            GameObject.Find("Ball manager").GetComponent<Ballmanger>().assingball(this.gameObject);
            myBall.transform.position = this.transform.position;
            circleInPlace = true;
        }
        playerdetected = false;

        encircling = true;
        destination = myBall.transform.position;
    }

    public override void ModifySpeed()
    {
        if (isattacking == true)
        {
            if (staggered <= 0)
            {
                speed = 6;
            }
        }

        if (staggered > 0)
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
        if (encircling == true && attacking == false)
        {
            if (myBall.GetComponent<circlefollow>().speed == 25)
            {
                speed = 15;
            }
        }
    }
    public void Attackcheck()
    {
        if (groupManager.isattacking == true)
        {
            if (attacking == false)
            {
                attackTimer = Random.Range(6, 10);
            }
        }
        if (angry == true)
        {
            attacking = true;
        }

        if (attacking == true)
        {
            enabled = true;
            if (speed < 8 && staggered <= 0)
            {
                speed += Time.deltaTime;
            }
            destination = player.transform.position;
        }
    }
}
