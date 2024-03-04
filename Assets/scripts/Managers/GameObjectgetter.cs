using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectgetter : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;
    public GameObject gun1;
    public GameObject gun2;
    public GameObject axe;
    public GameObject sword;
    public GameObject gunposition;

    [Header("Managers")]
    public GameObject Soundmanager;
    public GameObject Pausemanager;
    public GameObject MenuManager;

    [Header("Canvas")]
    public GameObject Toplefticons;
    public GameObject Healthbar;
    public GameObject Hurtscreen;
    public GameObject reloadingImage;
    public GameObject axecharge;
    public GameObject hitmarker;
    public GameObject grenadecounter;
    public GameObject ammo;
    public GameObject maxammo;
    public GameObject bulleticon;
    public GameObject gameovermenu;
    public GameObject gamepausedmenu;
    public GameObject controlsmenu;
    public GameObject WeaponWheelMenu;
    public GameObject reticle;
    public GameObject textDisplay;


    [Header("Camera things")]
    public GameObject cam;
    public Transform defaultaimpos;
    public Transform playerorientation;
    public GameObject aimpoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
