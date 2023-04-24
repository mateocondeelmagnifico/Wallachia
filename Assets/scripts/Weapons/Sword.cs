using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject bloodVFX;
    public GameObject player;

    public float axedamage; 

    public bool candamage;
    public bool isaxe;

    public Attack ataque;
    // Start is called before the first frame update
    void Start()
    {
        axedamage = 3;
        ataque = player.GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") && ataque.candamage == true )
        {
            Enemy enemigo = other.GetComponent<Enemy>();
            if (isaxe == false)
            {
                enemigo.takedamage(1.5f, "Light");
                enemigo.statuseffect("Iron");
                bloodVFX.GetComponent<ParticleSystem>().Emit(50);
            }
            else
            {
                enemigo.takedamage(axedamage, "Heavy");
                enemigo.statuseffect("Silver");
                bloodVFX.GetComponent<ParticleSystem>().Emit(100);
            }
        
        }
    }
}
