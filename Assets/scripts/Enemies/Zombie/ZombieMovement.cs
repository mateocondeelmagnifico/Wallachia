using UnityEngine;

namespace EnemyMechanics
{

    public class ZombieMovement : BasicEnemyMovement
    {
        public override void SetStartValues()
        {
            speed = 2;
            attackingrange = 2;
            lungespeed = 6;
        }
    }
}
