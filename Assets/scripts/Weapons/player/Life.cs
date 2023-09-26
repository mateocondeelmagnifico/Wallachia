using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public float health;
    public float currenthealth;
    public float bloodtimer;

    
    Image bloodyscreen;

    public bool riflereloaded;
    public bool pistolreloaded;

    public GameObjectgetter getter;
    GameObject rifle;
    GameObject pistol;
    GameObject gameovermenu;
    GameObject hurtscreen;
    GameObject reloadingtext;
    GameObject camera;
    GameObject reticle;
    Sonido soundManager;
    MenuManager menuManager;

    weapons armas;

    void Start()
    {
        armas = GetComponent<weapons>();
        soundManager = getter.Soundmanager.GetComponent<Sonido>();
        hurtscreen = getter.Hurtscreen;
        rifle = getter.gun2;
        pistol = getter.gun1;
        gameovermenu = getter.gameovermenu;
        reloadingtext = getter.reloadingtext;
        camera = getter.cam;
        reticle = getter.reticle;
        menuManager = getter.MenuManager.GetComponent<MenuManager>();

        riflereloaded = true;
        pistolreloaded = true;
        bloodyscreen = hurtscreen.GetComponent<Image>();
        bloodyscreen.color = new Color(1, 1, 1, 0);
        health = 6;
        currenthealth = health;
    }

    
    void Update()
    {
        //This is for the bloody screen when you get hurt
        if (health < currenthealth)
        {
            bloodtimer = 1;
            soundManager.playaudio("Player Hit");
            currenthealth = health;
        }
        if (bloodtimer > 0)
        {
            bloodtimer -= Time.deltaTime;
        }
        bloodyscreen.color = new Color(1, 1, 1, bloodtimer);

        //This is for when you die
        if (health <= 0)
        {
            //this is so you respawn with the same weapons
            #region Set Weapons
            RespawnManager manager = FindObjectOfType<RespawnManager>();
            manager.currentgrenade = armas.currentEquip[2];
            manager.currentgun = armas.currentEquip[1];
            manager.currentmelee = armas.currentEquip[0];
            #endregion

            #region Disable things
            getter.Pausemanager.SetActive(false);
            camera.GetComponent<Throwinggrenade>().enabled = false;
            reticle.SetActive(false);
            camera.GetComponent<Camara>().enabled = false;
            reloadingtext.SetActive(false);
            getter.textDisplay.SetActive(false);
            GetComponent<movement>().enabled = false;
            GetComponent<Attack>().enabled = false;
            GetComponent<weapons>().enabled = false;
            GetComponent<Restart>().canrestart = true;
            pistol.SetActive(false);
            rifle.SetActive(false);
            menuManager.changemenu("game over");
            this.enabled = false;
            #endregion
        }
    }
}
