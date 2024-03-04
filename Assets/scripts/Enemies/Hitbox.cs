using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyMechanics
{
    public class Hitbox : MonoBehaviour
    {
        public Enemy myEnemy;
        public float extraDamage;
        public void DealDamage(float damage, string type, string status)
        {
            Debug.Log(extraDamage);
            //This is for when a bullet collides with the enemy in the arm, it still counts
            myEnemy.TakeDamage(damage + extraDamage, type, true);
            myEnemy.StatusEffect(status);
        }
    }
}
