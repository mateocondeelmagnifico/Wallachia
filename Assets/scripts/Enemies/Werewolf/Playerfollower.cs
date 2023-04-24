using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfollower : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
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
