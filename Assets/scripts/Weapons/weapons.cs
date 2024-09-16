using UnityEngine;
using UnityEngine.UI;
using PlayerMechanics;

namespace WeaponMechanics
{
    public class weapons : MonoBehaviour
    {
        //This script takes care of weapon switching 
        //It also keeps track of what weapons you have equipped and which weaponds you have unlocked
        //It also changes the UI Icon of the bullets 

        //The temporary weapon is the one that is currently selected in the weapon wheel

        //public int[] currentEquip;
        //public int[] tempEquip;

        public GameObjectgetter getter;
        public GameObject[] armasMelee;
        public GameObject[] armasRango;
        public GameObject leftEquip, rightEquip;
        public string tempEquipLeft, tempRightEquip, tempGrenade;
        private Image bulleticon;
        GameObject soundmanager;
        public Animator animador;
        public Sprite[] bullets;
        public Image[] highlights;

        Sonido sound;
        RespawnManager resManager;
        private Attack attackScript;
        private Shooting shootingScript;
        private InputManagerPlayer inputmanager;
        private SetUiValues uiScript;

        public bool equippingweapon, isreloading, hassword, hasBullet, hasCross,hasrifle, hasaxe, hasgrenade, isattacking;

        public enum bulletTypes { iron, silver, sacred}
        public bulletTypes currentBullet;

        void Start()
        {
            if (RespawnManager.instance != null)
            {
                resManager = RespawnManager.instance;
                resManager.playerweapons = this;
            }
            bulleticon = getter.bulleticon.GetComponent<Image>();
            soundmanager = getter.Soundmanager;
            sound = soundmanager.GetComponent<Sonido>();
            inputmanager = GetComponent<InputManagerPlayer>();
            uiScript = GetComponent<SetUiValues>();
            attackScript = GetComponent<Attack>();

            #region Get unlocked weapons
            attackScript.hasmelee = false;
            if (resManager.unlockedThings.Contains("Bullets")) UnlockWeapon("Bullets");
            if (resManager.unlockedThings.Contains("Sword")) UnlockWeapon("Sword");
            if (resManager.unlockedThings.Contains("Cross")) UnlockWeapon("Cross");

            if (resManager.currentmelee != "")tempRightEquip = resManager.currentmelee;
            if (resManager.currentgun != "") tempEquipLeft = resManager.currentgun;
            else tempEquipLeft = "pistol";
            //currentEquip[2] = resManager.currentgrenade;

            ChangeWeapon(tempRightEquip);
            ChangeWeapon(tempEquipLeft);
            #endregion

            highlights[2].enabled = false;
            currentBullet = bulletTypes.iron;
            bulleticon.sprite = bullets[0];
            armasMelee[0].SetActive(false);
           
        }

        void Update()
        {
            #region see if you can shoot

            if (attackScript.attacking == true)
            {
                shootingScript.canshoot = false;
            }
            else
            {
                shootingScript.canshoot = true;
            }
            #endregion

            #region see if you can melee atack       

            //check if you can melee attack
            if (shootingScript.shotcooldown2 > 0)
            {
                attackScript.canattack = false;
            }
            else
            {
                attackScript.canattack = true;
            }
            #endregion

        }
        public void equipfinished()
        {
            equippingweapon = false;
            GetComponent<movement>().canmove = true;

            //This is so you respawn with the same weapons
            if(rightEquip!= null) resManager.currentmelee = rightEquip.name;
            resManager.currentgun = leftEquip.name;
            resManager.currentgrenade = tempGrenade;
            shootingScript.bulletType = currentBullet.GetHashCode();
        }
        public void equippoint()
        {
            if(rightEquip != null) rightEquip.SetActive(true);
            if(leftEquip != null) leftEquip.SetActive(true);
        }

        public void playsound()
        {
            sound.playaudio("Pickup", null);
        }
        public void SetiInactive()
        {
            armasMelee[0].SetActive(false);
            armasMelee[0].SetActive(false);
        }
        public void ChangeWeapon(string type)
        {
            //This code changes the weapons you have equipped

            if (type == "sword" || type == "axe")
            {
                //In currentequip is the int that defines the weapon that was previously equipped
                //In tempvalue is the weapon you want to equip
                sound.playaudio("Weapon Switch", null);
                animador.SetTrigger("Equip");
                equippingweapon = true;

                if (rightEquip != null) rightEquip.SetActive(false);
                if (tempRightEquip == "sword") rightEquip = armasMelee[0];
                else rightEquip = armasMelee[1];
                attackScript.currentweapon = rightEquip.name;
                attackScript.currentSword = rightEquip.GetComponent<Sword>();
            }
            if (type == "pistol" || type == "rifle")
            {
                sound.playaudio("Weapon Switch", null);
                animador.SetTrigger("Equip");
                equippingweapon = true;

                if(leftEquip != null) leftEquip.SetActive(false);
                if (tempEquipLeft == "pistol") leftEquip = armasRango[0];
                else leftEquip = armasRango[1];
                shootingScript = leftEquip.GetComponent<Shooting>();
            }

        }
        public void SetTempWeapon(string type)
        {
            //This script is accesed by the weapon wheel
            //It is used to keep track of which weapon the player is selecting

            if (type == "sword" || type == "axe")
            {
                tempRightEquip = type;
            }

            if (type == "pistol" || type == "rifle")
            {
                tempEquipLeft = type;
            }

            //These two work differently because there's only one icon to press in the weapon wheel
            /*
            if (type == "grenade")
            {
                tempEquip[2]++;
                if (tempEquip[2] >= 2)
                {
                    tempEquip[2] = 0;
                }
            }
            */
        }
        public void checkUnpause()
        {
            //Este código se ejecuta desde el menu de pausa

            if(rightEquip == null) ChangeWeapon(tempRightEquip);
            else if (rightEquip.name != tempRightEquip)
            {
                ChangeWeapon(tempRightEquip);
            }

            if (leftEquip.name != tempEquipLeft)
            {
                ChangeWeapon(tempEquipLeft);
            }
        }
        public void SetHighlight(string type)
        {
            //Esto lo llaman los botones de la weapon wheel

            switch (type)
            {
                case "Sword":
                    highlights[2].enabled = true;
                    highlights[3].enabled = false;
                    break;

                case "Axe":
                    highlights[2].enabled = false;
                    highlights[3].enabled = true;
                    break;

                case "Pistol":
                    highlights[0].enabled = true;
                    highlights[1].enabled = false;
                    break;

                case "Rifle":
                    highlights[0].enabled = false;
                    highlights[1].enabled = true;
                    break;
            }
        }
        public void ChangeBullet()
        {
            int myType = 0;
            string name = "";

           switch(currentBullet)
            {
                case bulletTypes.iron:
                    currentBullet = bulletTypes.silver;
                    myType = 1;
                    name = "Silver";
                    break;

                case bulletTypes.silver:
                    currentBullet = bulletTypes.sacred;
                    myType = 2;
                    name = "Sacred";
                    break;

                case bulletTypes.sacred:
                    currentBullet = bulletTypes.iron;
                    myType = 0;
                    name = "Iron";
                    break;
            }

            bulleticon.sprite = bullets[myType];
            shootingScript.bulletType = myType;
            uiScript.SetBulletName(name);
        }
        public void UnlockWeapon(string whichOne)
        {
            switch(whichOne)
            {
                case "Cross":
                    inputmanager.hasCross = true;
                    hasCross = true;
                    uiScript.crossImage.gameObject.SetActive(true);
                    resManager.unlockedThings.Add(whichOne);
                    break;

                case "Sword":
                    tempRightEquip = "sword";
                    attackScript.hasmelee = true;
                    hassword = true;
                    ChangeWeapon("sword");
                    resManager.unlockedThings.Add(whichOne);
                    break;

                case "Axe":
                    hasaxe = true;
                    resManager.unlockedThings.Add(whichOne);
                    break;

                case "Rifle":
                    hasrifle = true;
                    resManager.unlockedThings.Add(whichOne);
                    break;

                case "Bullets":
                    inputmanager.hasBullets = true;
                    hasBullet = true;
                    resManager.unlockedThings.Add(whichOne);
                    break;
            }
        }
    }
}
