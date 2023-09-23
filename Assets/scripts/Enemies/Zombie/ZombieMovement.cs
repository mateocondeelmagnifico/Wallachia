using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : BasicEnemyMovement
{

    public override void Start()
    {
        sonido = GetComponent<Sonido>();
        destination = this.transform.position;
        animador = GetComponent<Animator>();
        navegador = GetComponent<NavMeshAgent>();
        speed = 2;
        attackingrange = 2;
        lungespeed = 6;
        
    }

}
