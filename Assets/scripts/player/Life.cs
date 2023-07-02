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

    void Start()
    {
        hurtscreen = getter.Hurtscreen;
        rifle = getter.gun2;
        pistol = getter.gun1;
        gameovermenu = getter.gameovermenu;
        reloadingtext = getter.reloadingtext;
        camera = getter.cam;
        reticle = getter.reticle;

        riflereloaded = true;
        pistolreloaded = true;
        bloodyscreen = hurtscreen.GetComponent<Image>();
        bloodyscreen.color = new Color(1, 1, 1, 0);
        health = 6;
        currenthealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < currenthealth)
        {
            bloodtimer = 1;
            currenthealth = health;
        }
        if (bloodtimer > 0)
        {
            bloodtimer -= Time.deltaTime;
        }
        bloodyscreen.color = new Color(1, 1, 1, bloodtimer);
        if (health <= 0)
        {
            camera.GetComponent<Throwinggrenade>().enabled = false;
            reticle.SetActive(false);
            camera.GetComponent<Camara>().enabled = false;
            reloadingtext.SetActive(false);
            GetComponent<movement>().enabled = false;
            GetComponent<Attack>().enabled = false;
            GetComponent<weapons>().enabled = false;
            GetComponent<Restart>().canrestart = true;
            pistol.SetActive(false);
            rifle.SetActive(false);
            gameovermenu.SetActive(true);
            this.enabled = false;
        }
    }
}
