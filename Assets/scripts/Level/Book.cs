using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    //Mini tutorials and lore entries for the players to read
    [Header("Put here the thing this book unlocks, use uppercase")]
    public string whatPowerUp;

    public GameObject pausemanager;
    public GameObject noteobject;
    public GameObject key;

    public Sprite mypage;
    Image image;

    Pause pausa;
    weapons armas;

    public bool isnear;
    bool isreading;

    // Start is called before the first frame update
    void Start()
    {
        image = noteobject.GetComponent<Image>();
        pausa = pausemanager.GetComponent<Pause>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isnear == true)
        {
            if (isreading == false)
            {
                read(mypage);
            }
            else
            {
                stopreading();
                GivePowerUp(whatPowerUp);
            }
         
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isreading == true)
        {
            stopreading();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            armas = other.GetComponent<weapons>();
            key.SetActive(true);
            isnear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isnear = false;
            key.SetActive(false);
        }
    }
    private void read(Sprite whatpage)
    {
        image.sprite = whatpage;
        pausa.isonmenu = true;
        pausa.pause("partial");
        image.enabled = true;
        isreading = true;
    }
    private void stopreading()
    {
        image.enabled = false;
        pausa.unpause();
        isreading = false;
    }
    public void GivePowerUp(string type)
    {
        if (whatPowerUp == "Rifle")
        {
            armas.hasrifle = true;
            armas.playsound();
        }
        if (whatPowerUp == "Axe")
        {
            armas.hasaxe = true;
            armas.playsound();
        }
        if (whatPowerUp == "Bullet")
        {
            armas.hasbullet = true;
            armas.playsound();
        }
        if (whatPowerUp == "Grenade")
        {
            armas.hasgrenade = true;
            armas.playsound();
        }
        if (whatPowerUp == "Sword")
        {
            armas.hassword = true;
            armas.playsound();
        }
    }
}
