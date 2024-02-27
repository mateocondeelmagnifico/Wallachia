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
        CharacterController controlador;
        Sonido sound;

        public GameObjectgetter getter;
        GameObject chargeimage;
        GameObject axe;
        GameObject sword;
        GameObject Soundmanager;

        public bool tooclose, hasmelee, ispaused, axeraised, candamage, attacking, canattack;

        public int currentweapon;
        private float chargetimer, slowMoTimer;
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

            #region Attack
            if (Input.GetKeyDown(KeyCode.Mouse1) && attacking == false && canattack == true && currentweapon == 0 && ispaused == false && hasmelee)
            {
                sword.GetComponent<Sword>().hasPlayedSound = false;
                sound.playaudio("Sword Swing");
                animador.SetTrigger("Attack");
                attacking = true;
            }

            //The axe can be charged, so it needs different code than the sword
            if (Input.GetKey(KeyCode.Mouse1) && attacking == false && canattack == true && currentweapon == 1 && ispaused == false)
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
                axe.GetComponent<Sword>().hasPlayedSound = false;
                animador.SetBool("Axestill", false);
                animador.SetTrigger("Loweraxe");
                attacking = true;
                axe.GetComponent<Sword>().axedamage += chargetimer * 2;
            }
            #endregion

            //esto mueve al jugador p'alante
            if (attacking == true || axeraised == true)
            {
                GetComponent<weapons>().isattacking = true;
            }
            else
            {
                GetComponent<weapons>().isattacking = false;
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

        public void ActivateSlowMo(float howIntense, float howLong)
        {
            Time.timeScale = howIntense;
            slowMoTimer = howLong;
        }
    }
}
