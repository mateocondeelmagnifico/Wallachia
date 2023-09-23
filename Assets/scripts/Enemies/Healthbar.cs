using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject parent;
    public Image imagen;
    GameObject player;
    void Start()
    {
        imagen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imagen.fillAmount = parent.GetComponent<BasicEnemy>().life / parent.GetComponent<BasicEnemy>().maxlife;

        //Look at player
        player = parent.GetComponent<BasicEnemyMovement>().player;
        transform.LookAt(player.transform.position);
    }
}
