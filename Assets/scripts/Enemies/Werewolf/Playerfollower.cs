using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfollower : MonoBehaviour
{
    //This is the gamobject that the werewolf balls rotate around
    public Transform player;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        transform.position = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
