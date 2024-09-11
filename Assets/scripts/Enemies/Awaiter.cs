using EnemyMechanics;
using PlayerMechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awaiter : MonoBehaviour
{
    //Script for enemies that stay still for some time
    [SerializeField] private float distance;
    private Transform player;

    private void Start()
    {
        player = Scaryness.Instance.transform;
    }

    private void Update()
    {
        //Check if player is near and self destruct
        if (Vector3.Distance(player.position,transform.position) < distance)
        {
            GetComponent<Enemy>().awaiting = false;
            Destroy(this);
        }
    }
}
