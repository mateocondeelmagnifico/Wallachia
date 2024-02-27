using UnityEngine;

namespace EnemyMechanics
{
    public class circlefollow : MonoBehaviour
    {
        //The script in each ball, this makes them rotate around the player
        Transform player;
        public bool istaken;
        public float speed;
        void Start()
        {
            speed = 15;
            player = GameObject.Find("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.RotateAround(player.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
