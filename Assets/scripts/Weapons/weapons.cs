using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapons : MonoBehaviour
{
    //This script takes care of weapon switching 
    //It also keeps track of what weapons you have equipped and which weaponds you have unlocked
    //It also changes the UI Icon of the bullets 

    //The temporary weapon is the one that is currently selected in the weapon wheel

    public int[] currentEquip;
    public int[] tempEquip;

    string weapon;

    public GameObjectgetter getter;
    public GameObject[] armas;
    public GameObject[] armasrango;
    GameObject bulleticon;
    GameObject soundmanager;
    public Animator animador;
    public Sprite[] bullets;

    Sonido sound;

    public bool equippingweapon;
    public bool isreloading;
    public bool hasrifle;
    public bool hasaxe;
    public bool hasgrenade;
    public bool hasbullet;
    public bool isattacking;
    void Start()
    {
        armas[0] = getter.sword;
        armas[1] = getter.axe;
        armasrango[0] = getter.gun1;
        armasrango[1] = getter.gun2;
        bulleticon = getter.bulleticon;
        soundmanager = getter.Soundmanager;
        sound = soundmanager.GetComponent<Sonido>();

        //weapon 0 = sword
        //weapon 1 = axe

        currentEquip = new int[4];
        currentEquip[2] = 0;
        //0 = melee weapon, 1 = ranged weapon, 2 = grenade, 3 = bullet
        tempEquip = new int[4];
    }

    // Update is called once per frame
    void Update()
    {
        #region see if you can shoot

        if (GetComponent<Attack>().attacking == true)
        {
            armasrango[currentEquip[1]].GetComponent<Shooting>().canshoot = false;
        }
        else
        {
            armasrango[currentEquip[1]].GetComponent<Shooting>().canshoot = true;
        }
        #endregion

        #region see if you can melee atack
        //check if you can melee attck
        if (armasrango[currentEquip[1]].GetComponent<Shooting>().shotcooldown2 > 0)
        {
            GetComponent<Attack>().canattack = false;
        }
        else
        {
            GetComponent<Attack>().canattack = true;
        }
        #endregion

        #region see what bullet icon to display
        if (currentEquip[3] == 0)
        {
            bulleticon.GetComponent<Image>().sprite = bullets[0];
        }
        else
        {
            bulleticon.GetComponent<Image>().sprite = bullets[1];
        }
        #endregion
    }
    public void equipfinished()
    {
        equippingweapon = false;
        GetComponent<movement>().canmove = true;
    }
    public void equippoint()
    {
        armas[currentEquip[0]].SetActive(true);
        armasrango[currentEquip[1]].SetActive(true);
    }
   
    public void playsound()
    {
        sound.playaudio("Pickup");
    }
    public void SetiInactive()
    {
        armas[currentEquip[0]].SetActive(false);
        armas[currentEquip[1]].SetActive(false);
    }
    public void ChangeWeapon(string type)
    {
        //This code changes the weapons you have equipped

        sound.playaudio("Weapon Switch");
       
        if (type == "melee")
        {
            //In currentequip is the int that defines the weapon that was previously equipped
            //In tempvalue is the weapon you want to equip
            animador.SetTrigger("Equip");
            equippingweapon = true;

            armas[currentEquip[0]].SetActive(false);
            armas[tempEquip[0]].SetActive(true);
        }
       if (type == "ranged")
        {
            animador.SetTrigger("Equip");
            equippingweapon = true;

            armasrango[currentEquip[1]].SetActive(false);
            armasrango[tempEquip[1]].SetActive(true);
        }
    }
    public void SetTempWeapon(string type)
    {
        //This script is accesed by the weapon wheel
        //It is used to keep track of which weapon the player is selecting

        if (type == "sword" && isattacking == false)
        {
            tempEquip[0] = 0;
        }
        if (type == "axe" && isattacking == false)
        {
            tempEquip[0] = 1;
        }
        if (type == "pistol" && isreloading == false)
        {
            tempEquip[1] = 0;
        }
        if (type == "rifle" && isreloading == false)
        {
            tempEquip[1] = 1;
        }

        //These two work differently because there's only one icon to press in the weapon wheel
        if (type == "grenade")
        {
            tempEquip[2]++;
            if (tempEquip[2] >= 2)
            {
                tempEquip[2] = 0;
            }
        }
        if (type == "bullet" && isreloading == false)
        {
            tempEquip[3]++;
            if (tempEquip[3] >= 2)
            {
                tempEquip[3] = 0;
            }
        }
    }
    public void checkUnpause()
    {
        //Este código se ejecuta desde el menu de pausa
        for(int i = 0; i < 4; i++)
        {
            if (currentEquip[i] != tempEquip[i])
            { 
                if(i == 0)
                {
                    weapon = "melee";
                }
                if(i == 1)
                {
                    weapon = "ranged";
                }
                if (i == 2)
                {
                    weapon = "";
                }
                if (i == 3)
                {
                    weapon = "";
                }
                ChangeWeapon(weapon);
                currentEquip[i] = tempEquip[i];
            }
        }
    }
}
