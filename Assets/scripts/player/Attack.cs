using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    //This script controls the melee attack both for the sword and the axe
    //It's in charge of attacktimers, UI and animations basically

    public Animator animador;
    CharacterController controlador;
    Sonido sound;

    public GameObjectgetter getter;
    GameObject chargeimage;
    GameObject axe;
    GameObject sword;
    GameObject Soundmanager;

    public bool tooclose;
    public bool canattack;
    public bool attacking;
    public bool candamage;
    public bool axeraised;
    public bool ispaused;

    public int currentweapon;
    float chargetimer;
    void Start()
    {
        chargeimage = getter.axecharge;
        axe = getter.axe;
        sword = getter.sword;
        Soundmanager = getter.Soundmanager;
        canattack = true;
        controlador = GetComponent<CharacterController>();
        sound = Soundmanager.GetComponent<Sonido>();
    }

    // Update is called once per frame
    void Update()
    {
        chargeimage.GetComponent<Image>().fillAmount = chargetimer / 1.2f;
        currentweapon = GetComponent<weapons>().currentEquip[0];

        if (Input.GetKeyDown(KeyCode.Mouse1) && attacking == false && canattack == true && currentweapon == 0 && ispaused == false)
        {
            sword.GetComponent<Sword>().hasPlayedSound = false;
            sound.playaudio("Sword Swing");
            animador.SetTrigger("Attack");
            attacking = true;
            GetComponent<movement>().canmove = false;
            
        }

        //The axe can be charged, so it needs different code than the sword
        if (Input.GetKey(KeyCode.Mouse1) && attacking == false && canattack == true && currentweapon == 1 && ispaused == false)
        {
            if (axeraised == false)
            {
                animador.SetTrigger("Raiseaxe");
                axeraised = true;
            }
            else
            {
                animador.SetBool("Axestill", true);
            }
            
            if (chargetimer < 1.2f)
            {
                chargetimer += Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && chargetimer > 0 && ispaused == false)
        {
            axe.GetComponent<Sword>().hasPlayedSound = false;
            animador.SetBool("Axestill", false);
            animador.SetTrigger("Loweraxe");
            attacking = true;
            GetComponent<movement>().canmove = false;
            axe.GetComponent<Sword>().axedamage += chargetimer * 2;
        }

        //esto mueve al jugador p'alante
        if (candamage == true)
        {
            if (tooclose == false)
            {
                controlador.Move(transform.forward * 10 * Time.deltaTime);
            }
        }
        if (attacking == true || axeraised == true)
        {
            GetComponent<weapons>().isattacking = true;
        }
        else
        {
            GetComponent<weapons>().isattacking = false;
        }
    }
    public void Attackend()
    {
        axeraised = false;
        attacking = false;
        GetComponent<movement>().canmove = true;
        chargetimer = 0;
        axe.GetComponent<Sword>().axedamage = 3;
    }
    public void Damageend()
    {
        candamage = false;
    }

    public void Attackstart()
    {
        candamage = true;
    }

}
