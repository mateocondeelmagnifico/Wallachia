using UnityEngine;
using UnityEngine.UI;
using WeaponMechanics;

namespace PlayerMechanics
{
    public class Attack : MonoBehaviour
    {
        //This script controls the melee attack both for the sword and the axe
        //It's in charge of attacktimers, UI and animations basically

        public Animator animador;
        Sonido sound;
        private weapons myWeapons;

        public GameObjectgetter getter;
        private Image chargeimage;
        public Sword currentSword;
        GameObject axe;
        GameObject sword;
        GameObject Soundmanager;

        public bool tooclose, hasmelee, ispaused, axeraised, candamage, attacking, canattack;

        public string currentweapon;
        private float chargetimer, slowMoTimer;
        void Start()
        {
            chargeimage = getter.axecharge.GetComponent<Image>();
            axe = getter.axe;
            sword = getter.sword;
            Soundmanager = getter.Soundmanager;
            canattack = true;
            sound = Soundmanager.GetComponent<Sonido>();
            myWeapons = GetComponent<weapons>();
            chargeimage.fillAmount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (chargetimer > 0) chargeimage.fillAmount = chargetimer / 1.2f;

            #region Attack
            if (Input.GetKeyDown(KeyCode.Mouse1) && attacking == false && canattack == true && currentweapon == "sword" && ispaused == false && hasmelee)
            {
                currentSword.hasPlayedSound = false;
                sound.playaudio("Sword Swing");
                animador.SetTrigger("Attack");
                attacking = true;
            }

            //The axe can be charged, so it needs different code than the sword
            if (Input.GetKey(KeyCode.Mouse1) && attacking == false && canattack == true && currentweapon == "axe" && ispaused == false)
            {
                if (axeraised == false)
                {
                    sound.playaudio("Raise Axe");
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
                currentSword.hasPlayedSound = false;
                animador.SetBool("Axestill", false);
                animador.SetTrigger("Loweraxe");
                attacking = true;
                currentSword.axedamage += chargetimer * 2.7f;
            }
            #endregion

            //esto mueve al jugador p'alante
            if (attacking == true || axeraised == true)
            {
                myWeapons.isattacking = true;
            }
            else
            {
                myWeapons.isattacking = false;
            }

            #region manage Slow Mo
            if (slowMoTimer > 0)
            {
                slowMoTimer -= Time.deltaTime;
            }
            else if (Time.timeScale != 1 && !ispaused)
            {
                Time.timeScale = 1;
            }
            #endregion
        }
        public void Attackend()
        {
            axeraised = false;
            attacking = false;
            //GetComponent<movement>().canmove = true;
            chargetimer = 0;
            currentSword.axedamage = 2;
        }
        public void Damageend()
        {
            candamage = false;
        }

        public void Attackstart()
        {
            candamage = true;
        }

        public void ActivateSlowMo(float howIntense, float howLong)
        {
            Time.timeScale = howIntense;
            slowMoTimer = howLong;
        }
    }
}
