using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballmanger : MonoBehaviour
{
    //The ball manager asings a ball to each werewolf that requests it

    public GameObject[] balls;
    public Transform player;

    int chosenball;
    int activeballs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distancecheck();
    }
    public void assingball(GameObject werewolf)
    {
        chosenball = 0;

            while (chosenball <= 6)
            {
                if (balls[chosenball].GetComponent<circlefollow>().istaken == false)
                {
                    balls[chosenball].SetActive(true);
                    werewolf.GetComponent<WerewolfMovement>().myBall = balls[chosenball];
                    balls[chosenball].GetComponent<circlefollow>().istaken = true;
                    chosenball = 7;
                    activeballs++;
                }
                else
                {
                    chosenball++;
                }
            }
    }
    public void distancecheck()
    {
        //This makes werewolves separate themselves if they are too close
        //It increases the speed of the rightmost werewolf
        if (activeballs > 0)
        {
            anglecheck(0, 1);
            if (activeballs == 2)
            {
                anglecheck(1, 2);
            }
        }
        
    }
    public void anglecheck(int ball1, int ball2)
    {
        int chosenball;
        int otherball;
        if (Vector3.Angle(balls[ball1].transform.position, player.position) > Vector3.Angle(balls[ball2].transform.position, player.position))
        {
            chosenball = ball1;
            otherball = ball2;
        }
        else
        {
            chosenball = ball2;
            otherball = ball1;
        }

        if (Vector3.Distance(balls[chosenball].transform.position, balls[otherball].transform.position) < 10)
        {
            balls[chosenball].GetComponent<circlefollow>().speed = 25;
        }
        else
        {
            balls[chosenball].GetComponent<circlefollow>().speed = 15;
        }
    }
}
