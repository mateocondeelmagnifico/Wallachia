using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerMechanics;

namespace WeaponMechanics
{
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
        public GameObject riflebullet, particlesobject;
        GameObject Ammocounter, Maxammocounter, Reloadingimage, sound;

        private Image reloadingCircle;

        ParticleSystem particles;
        GameObject luz;
        GameObject mybullet;

        Rigidbody cuerporigido;
        private Camara myCam;

        public bool missingammo;
        public bool canshoot;
        public bool isinplace;
        public bool ispaused;
        bool reloading;
        public bool canreload;
        public bool isrunning;
        public bool keypressed;

        public Transform cannon;
        public Transform gunposition;
        public Transform reloadingpoint;

        public float shotcooldown;
        public float shotcooldown2, recoil, damage;
        private float reloadingtimer;
        float lighttimer;
        float multiplier;

        public int ammo, clipAmmo, maxammo, bulletType;

        public Vector3 aim;
        Vector3 tempposition;

        void Start()
        {
            player = getter.Player;
            camara = getter.cam;
            aimpoint = getter.aimpoint;
            Ammocounter = getter.ammo;
            Maxammocounter = getter.maxammo;
            Reloadingimage = getter.reloadingImage;
            reloadingCircle = Reloadingimage.GetComponent<Image>();
            sound = getter.Soundmanager;
            myCam = camara.GetComponent<Camara>();

            sonido = sound.GetComponent<Sonido>();
            vida = player.GetComponent<Life>();
            canshoot = true;
            cuerporigido = GetComponent<Rigidbody>();
            particles = particlesobject.GetComponent<ParticleSystem>();
            luz = particles.transform.GetChild(1).gameObject;

            ammo = clipAmmo;

            SetAmmoCounter(0);
            SetAmmoCounter(1);

            transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        void Update()
        {
            checkgun();

            //recoil 

            /*
            if (shotcooldown2 <= 0.3f && !isinplace)
            {
                transform.position = Vector3.MoveTowards(transform.position, gunposition.position, 3 * Time.deltaTime);
                cuerporigido.velocity = new Vector3(0, 0, 0);
            }
            

            if (transform.position == gunposition.position && shotcooldown2 <= 0.5f)
            {
                isinplace = true;
            }
            */

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
            if (clipAmmo == 4)
            {
                if (vida.riflereloaded == false)
                {
                    maxammo = 20;
                    ammo = 4;
                    vida.riflereloaded = true;
                }
            }
            else
            {
                if (vida.pistolreloaded == false)
                {
                    maxammo = 35;
                    ammo = 6;
                    vida.pistolreloaded = true;
                }
            }
        }
        public void checkreload()
        {
            if (reloading == false && canshoot == true)
            {
                if(ammo < clipAmmo)
                {
                    canreload = true;
                }
            }

            if (reloadingtimer > 0)
            {
                reloadingtimer -= Time.deltaTime;
                reloadingCircle.fillAmount = 1 - reloadingtimer/4;
            }
            else if (reloading == true)
            {
                ammo = clipAmmo;
                reloading = false;
                missingammo = false;
                reloadingCircle.fillAmount = 0;
                SetAmmoCounter(0);
            }
        }
        public void shoot()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && reloading == false && canshoot == true)
            {
                if (ammo <= 0)
                {
                    sonido.playaudio("dont shoot");
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && shotcooldown <= 0 && canshoot == true && reloading == false && ammo > 0)
            {
                myCam.ShakeCam(2);
                instantiatebullet();
                ammo--;
                particles.Emit(10);
                lighttimer = 0.1f;
                shotcooldown = recoil;
            }

            SetAmmoCounter(0);
        }
        public void instantiatebullet()
        {

           mybullet = GameObject.Instantiate(bullet, cannon.position, transform.rotation);
           mybullet.GetComponent<Bullet>().damage = damage;
           mybullet.GetComponent<Bullet>().myType = bulletType;

            shotcooldown2 = 0.45f;
            sonido.playaudio("shoot");
            missingammo = true;
            //isinplace = false;
            //cuerporigido.AddForce(transform.forward.normalized * -200);
        }
        public void reload()
        {
            if (Input.GetKeyDown(KeyCode.R) && canreload == true)
            {
                if (maxammo > 0)
                {
                    sonido.playaudio("Reload");
                    reloadingmath("Iron");
                    reloading = true;
                    reloadingtimer = 4;
                }

                SetAmmoCounter(1);
            }
        }
        public void reloadingmath(string type)
        {

                    if (maxammo >= clipAmmo)
                    {
                        maxammo -= clipAmmo - ammo;
                    }
                    else
                    {
                        while (ammo < clipAmmo && maxammo > 0)
                        {
                            maxammo--;
                            ammo++;
                        }
                    }
        }
        private void SetAmmoCounter(int type)
        {
            if (type == 0) Ammocounter.GetComponent<Text>().text = ammo.ToString();
            if (type == 1) Maxammocounter.GetComponent<Text>().text = maxammo.ToString();
        }

    }
}
