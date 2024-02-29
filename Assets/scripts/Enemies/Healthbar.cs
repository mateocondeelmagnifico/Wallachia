using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyMechanics
{
    public class Healthbar : MonoBehaviour
    {
        public GameObject parent, X;
        public SpriteRenderer imagen;
        private Transform aimPoint;

        private Enemy myenemy;

        private bool doOnce;

        private float timer;
        void Start()
        {
            imagen = GetComponent<SpriteRenderer>();
            myenemy = parent.GetComponent<Enemy>();
            timer = 0.75f;
        }

        void Update()
        {
            if (!doOnce)
            {
                aimPoint = parent.GetComponent<Enemy>().player.transform.GetChild(2);
                doOnce = true;
            }

            //Change the color of the sprite
            float colorGradient = (myenemy.life / myenemy.maxLife);
            imagen.color = new Color(1, colorGradient, colorGradient, 1);

            //Look at player
            transform.LookAt(aimPoint.position);

            if (myenemy.life <= 0)
            {
                X.SetActive(true);
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    imagen.enabled = false;
                    Destroy(X);
                    Destroy(this);
                }
            }
        }
    }
}
