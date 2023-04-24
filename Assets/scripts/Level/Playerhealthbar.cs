using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerhealthbar : MonoBehaviour
{
    public GameObject player;
    Life vida;
    Image imagen;
    void Start()
    {
        imagen = GetComponent<Image>();
        vida = player.GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        imagen.fillAmount = vida.health / 6;
    }
}
