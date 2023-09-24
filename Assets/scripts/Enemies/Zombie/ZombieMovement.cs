using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : BasicEnemyMovement
{
    public override void SetStartValues()
    {
        speed = 2;
        attackingrange = 2;
        lungespeed = 6;
    }
}
