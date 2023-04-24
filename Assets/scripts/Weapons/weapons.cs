using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapons : MonoBehaviour
{
    public int currentmeleeweapon;
    public int currentrangeweapon;
    public int currentgrenade;

    public string currentbullet;

    public GameObject[] armas;
    public GameObject[] armasrango;
    public GameObject bulleticon;
    public GameObject soundmanager;
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
        sound = soundmanager.GetComponent<Sonido>();
        currentbullet = "Iron";
        currentmeleeweapon = 0;
        currentrangeweapon = 0;

        //weapon 0 = sword
        //weapon 1 = axe
    }

    // Update is called once per frame
    void Update()
    {
        if (hasrifle == true)
        {
            checkrangedchange();
        }
        if (hasaxe == true)
        {
            checkmeleechange();
        }
        if (hasbullet == true)
        {
            checkbulletchange();
        }
        if (hasgrenade == true)
        {
            checkgrenadechange();
        }

        //check if you can shoot
        if (GetComponent<Attack>().attacking == true)
        {
            armasrango[currentrangeweapon].GetComponent<Shooting>().canshoot = false;
        }
        else
        {
            armasrango[currentrangeweapon].GetComponent<Shooting>().canshoot = true;
        }
        //check if you can melee attck
        if (armasrango[currentrangeweapon].GetComponent<Shooting>().shotcooldown2 > 0)
        {
            GetComponent<Attack>().canattack = false;
        }
        else
        {
            GetComponent<Attack>().canattack = true;
        }
    }
    public void equipfinished()
    {
        equippingweapon = false;
        GetComponent<movement>().canmove = true;
    }
    public void equippoint()
    {
        armas[currentmeleeweapon].SetActive(true);
        armasrango[currentrangeweapon].SetActive(true);
    }
    public void checkrangedchange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && equippingweapon == false && isreloading == false)
        {
            animador.SetTrigger("Equip");
            equippingweapon = true;

            armasrango[currentrangeweapon].SetActive(false);

            currentrangeweapon++;

            if (currentrangeweapon >= 2)
            {
                currentrangeweapon = 0;
            }
        }
    }
    public void checkmeleechange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && equippingweapon == false && isattacking == false)
        {
            animador.SetTrigger("Equip");
            equippingweapon = true;

            armas[currentmeleeweapon].SetActive(false);

            currentmeleeweapon++;

            if (currentmeleeweapon >= 2)
            {
                currentmeleeweapon = 0;
            }
        }
    }
    public void checkbulletchange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentbullet == "Iron")
            {
                currentbullet = "Silver";
                bulleticon.GetComponent<Image>().sprite = bullets[1];
            }
            else
            {
                currentbullet = "Iron";
                bulleticon.GetComponent<Image>().sprite = bullets[0];
            }
        }
    }
    public void checkgrenadechange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentgrenade++;
            if (currentgrenade > 1)
            {
                currentgrenade = 0;
            }
        }
    }
    public void playsound()
    {
        sound.playaudio("Pickup");
    }
}
