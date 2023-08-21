using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    //THis script is in charge of shooting and reloading both for the revolver and the rifle
    //You can only whoot if you're not reloading, changing weapons or haven't done a melee attack recently

    Life vida;
    Sonido sonido;

    public GameObjectgetter getter;
    GameObject player;
    public GameObject bullet;
    GameObject camara;
    GameObject aimpoint;
    public GameObject riflebullet;
    GameObject Ammocounter;
    GameObject Maxammocounter;
    GameObject Reloadingimage;
    public GameObject particlesobject;
    GameObject sound;

    ParticleSystem particles;
    GameObject luz;
    GameObject mybullet;

    Rigidbody cuerporigido;
    weapons armas;

    public bool isrifle;
    public bool missingammo;
    public bool canshoot;
    public bool isinplace;
    public bool silvermode;
    public bool ispaused;
    bool reloading;
    public bool canreload;
    public bool isrunning;
    public bool keypressed;

    public Transform cannon;
    public Transform gunposition;
    public Transform reloadingpoint;

    float shotcooldown;
    public float shotcooldown2;
    float reloadingtimer;
    float lighttimer;
    float multiplier;

    public int ammo;
    public int maxammo;
    public int silverammo;
    public int maxsilverammo;
    
    
    public Vector3 aim;
    Vector3 tempposition;

    void Start()
    {
        player = getter.Player;
        camara = getter.cam;
        aimpoint = getter.aimpoint;
        Ammocounter = getter.ammo;
        Maxammocounter = getter.maxammo;
        Reloadingimage = getter.reloadingtext;
        sound = getter.Soundmanager;

      armas = player.GetComponent<weapons>();
      sonido = sound.GetComponent<Sonido>();
      vida = player.GetComponent<Life>();
      canshoot = true;
      cuerporigido = GetComponent<Rigidbody>();
      particles = particlesobject.GetComponent<ParticleSystem>();
      luz = particles.transform.GetChild(1).gameObject;

      if(isrifle == true)
      {
        ammo = 4;
        maxammo = 20;
      }
      else
      {
        ammo = 6;
        maxammo = 35;
      }
      maxsilverammo = 6;
       transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        //If the bullet is silver
        if (armas.currentEquip[3] == 1)
        {
            silvermode = true;
        }
        else
        {
            silvermode = false;
        }
        checkgun();

        //recoil
        if (transform.position == gunposition.position && shotcooldown <= 0.3f)
        {
            isinplace = true; 
        }
        if (isinplace == false)
        {
            if (isrifle == false && shotcooldown <= 0.3f || isrifle == true && shotcooldown <= 1.8f)
            {
                transform.position = Vector3.MoveTowards(transform.position, gunposition.position, 3 * Time.deltaTime);
                cuerporigido.velocity = new Vector3(0, 0, 0);
            }
        }

        if (silvermode == false)
        {
            Ammocounter.GetComponent<Text>().text = ammo.ToString();
            Maxammocounter.GetComponent<Text>().text = maxammo.ToString();
        }
        else
        {
            Ammocounter.GetComponent<Text>().text = silverammo.ToString();
            Maxammocounter.GetComponent<Text>().text = maxsilverammo.ToString();
        }

        if (ispaused == false && isrunning == false)
        {
            shoot();
        }
  
        canreload = false;

       checkreload();
       reload();
       
        if (shotcooldown > 0)
        {
            shotcooldown -= Time.deltaTime;
        }
        shotcooldown2 -= Time.deltaTime;

        Vector3 mouseposition = Input.mousePosition;

        mouseposition += Camera.main.transform.forward * 30;

        //Gun goes up while running
        if (reloading == false && isrunning == false)
        {
           transform.LookAt(aimpoint.transform.position);
           tempposition = aimpoint.transform.position;
        }
        else
        {
            if (keypressed || reloading)
            {
                //raise up gun
                if (multiplier < 1)
                {
                    multiplier += Time.deltaTime;
                }

                tempposition = Vector3.Lerp(tempposition, reloadingpoint.position, multiplier);
            }
            else
            {
                if (multiplier > 0)
                {
                    multiplier -= Time.deltaTime * 5;
                }
                tempposition = Vector3.Lerp(tempposition, aimpoint.transform.position, multiplier);
            }
            
            transform.LookAt(tempposition);
        }

        player.GetComponent<weapons>().isreloading = reloading;

        //Light particle effects
        if (lighttimer > 0)
        {
            luz.GetComponent<Light>().enabled = true;
            lighttimer -= Time.deltaTime;
        }
        else
        {
            luz.GetComponent<Light>().enabled = false;
        }

    }
    public void checkgun()
    {
        if (isrifle == true)
        {
            if (vida.riflereloaded == false)
            {
                maxammo = 20;
                ammo = 4;
                maxsilverammo = 7;
                vida.riflereloaded = true;
            }
        }
        else
        {
            if (vida.pistolreloaded == false)
            {
                maxammo = 35;
                ammo = 6;
                maxsilverammo = 7;
                vida.pistolreloaded = true;
            }
        }
    }
    public void checkreload()
    {
        if (reloading == false && canshoot == true)
        {
            if (silvermode == false)
            {
                if (isrifle == true && ammo < 4)
                {
                    canreload = true;
                }
                if (isrifle == false && ammo < 6)
                {
                    canreload = true;
                }
            }
            else
            {
                if (isrifle == true && silverammo < 4)
                {
                    canreload = true;
                }
                if (isrifle == false && silverammo < 6)
                {
                    canreload = true;
                }
            }
        }
        if (reloadingtimer > 0)
        {
            reloadingtimer -= Time.deltaTime;
        }
        else
        {
            if (reloading == true)
            {
                if (silvermode == false)
                {
                    if (isrifle == true)
                    {
                        ammo = 4;
                    }
                    else
                    {
                        ammo = 6;
                    }
                }
                else
                {
                    if (isrifle == true)
                    {
                        silverammo = 4;
                    }
                    else
                    {
                        silverammo = 6;
                    }
                }
                reloading = false;
                missingammo = false;
                Reloadingimage.GetComponent<Text>().enabled = false;
            }
        }
    }
    public void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && reloading == false && canshoot == true)
        {
            if (silvermode == false && ammo <=0 || silvermode == true && silverammo <= 0)
            {
                sonido.playaudio("dont shoot");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && shotcooldown <= 0 && canshoot == true && reloading == false)
        {
            if (silvermode == false && ammo > 0)
            {
                instantiatebullet();
                ammo--;
                particles.Emit(10);
                lighttimer = 0.1f;
            }  
            if (silvermode == true && silverammo > 0)
            {
                instantiatebullet();
                silverammo--;
                particles.Emit(10);
                lighttimer = 0.1f;
            }
        }
    }
    public void instantiatebullet()
    {
        if (isrifle == true)
        {
            shotcooldown = 2;
            mybullet = GameObject.Instantiate(riflebullet, cannon.position, transform.rotation);
            mybullet.GetComponent<Bullet>().armas = armas;
            mybullet.GetComponent<Bullet>().player = player.transform;
        }
        else
        {
            shotcooldown = 0.4f;
            mybullet = GameObject.Instantiate(bullet, cannon.position, transform.rotation);
            mybullet.GetComponent<Bullet>().armas = armas;
            mybullet.GetComponent<Bullet>().player = player.transform;
        }
        shotcooldown2 = 0.45f;
        sonido.playaudio("shoot");
        missingammo = true;
        isinplace = false;
        cuerporigido.AddForce(transform.forward.normalized * -200);
    }
    public void reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && canreload == true)
        {
            if (silvermode == false && maxammo > 0)
            {
                sonido.playaudio("Reload");
                reloadingmath("Iron");
                Reloadingimage.GetComponent<Text>().enabled = true;
                reloading = true;
                reloadingtimer = 4;
            }
            if (silvermode == true && maxsilverammo > 0)
            {
                sonido.playaudio("Reload");
                reloadingmath("Silver");
                Reloadingimage.GetComponent<Text>().enabled = true;
                reloading = true;
                reloadingtimer = 4;
            }
        }
    }
    public void reloadingmath(string type)
    {
        if (type == "Silver")
        {
            if (isrifle == true)
            {
                if (maxsilverammo >= 4)
                {
                    maxsilverammo -= 4 - silverammo;
                }
                else
                {
                    while (silverammo < 4 && maxsilverammo > 0)
                    {
                        maxsilverammo--;
                        silverammo++;
                    }
                }
            }
            else
            {
                if (maxsilverammo >= 6)
                {
                    maxsilverammo -= 6 - silverammo;
                }
                else
                {
                    while (silverammo < 6 && maxsilverammo > 0)
                    {
                        maxsilverammo--;
                        silverammo++;
                    }
                }
            }
        }
        else
        {
            if (isrifle == true)
            {
                if (maxammo >= 4)
                {
                    maxammo -= 4 - ammo;
                }
                else
                {
                    while (ammo < 4 && maxammo > 0)
                    {
                        maxammo--;
                        ammo++;
                    }
                }
            }
            else
            {
                if (maxammo >= 6)
                {
                    maxammo -= 6 - ammo;
                }
                else
                {
                    while (ammo < 6 && maxammo > 0)
                    {
                        maxammo--;
                        ammo++;
                    }
                }
            }
        }
    }
}
