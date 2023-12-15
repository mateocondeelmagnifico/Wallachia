using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject parent;
    public Image imagen;
    private GameObject player;

    private BasicEnemy myenemy;

    private bool doOnce;

    private float timer;
    void Start()
    {
        imagen = GetComponent<Image>();
        myenemy = parent.GetComponent<BasicEnemy>();
        timer = 0.75f;
    }
    
    void Update()
    {
        if(!doOnce)
        {
            player = parent.GetComponent<BasicEnemyMovement>().player;
            doOnce = true;
        }

        //Change the color of the sprite
        float colorGradient = (myenemy.life / myenemy.maxlife);
        imagen.color = new Color(1, colorGradient, colorGradient, 1);

        //Look at player
        transform.LookAt(player.transform.position);

        if(myenemy.life <= 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                imagen.enabled = false;
                Destroy(this);
            }
        }
    }
}
