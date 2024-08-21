using UnityEngine;
using UnityEngine.UI;
using WeaponMechanics;
using static UnityEngine.PlayerLoop.PreUpdate;

namespace PlayerMechanics
{
    public class Life : MonoBehaviour
    {
        public float health;
        public float currenthealth;

        public bool riflereloaded;
        public bool pistolreloaded;

        public GameObjectgetter getter;
        GameObject rifle;
        GameObject pistol;
        GameObject reloadingtext;
        GameObject camara;
        GameObject reticle;
        Sonido soundManager;
        MenuManager menuManager;
        private SetUiValues uiUpdater;
        private Camara myCam;

        weapons armas;

        void Start()
        {
            armas = GetComponent<weapons>();
            soundManager = getter.Soundmanager.GetComponent<Sonido>();
            rifle = getter.gun2;
            pistol = getter.gun1;
            reloadingtext = getter.reloadingImage;
            camara = getter.cam;
            reticle = getter.reticle;
            menuManager = getter.MenuManager.GetComponent<MenuManager>();
            uiUpdater = GetComponent<SetUiValues>();
            myCam = camara.GetComponent<Camara>();

            riflereloaded = true;
            pistolreloaded = true;
            
            health = 6;
            currenthealth = health;
        }

        public void ChangeLife(float amount)
        {
            float Amount = amount;
            if (health + amount > 6) Amount = 6 - health;

            if (amount < 0)
            {
                soundManager.playaudio("Player Hit");
                myCam.ShakeCam(-(Mathf.RoundToInt(amount)));
            }
            health += amount;
            if(health > 6) health = 6;

            uiUpdater.UpdateBloodyScreen(Amount);

            //This is for when you die
            if (health <= 0)
            {
                //this is so you respawn with the same weapons
                #region Set Weapons
                RespawnManager manager = FindObjectOfType<RespawnManager>();
                manager.currentgrenade = armas.tempGrenade;
                manager.currentgun = armas.leftEquip.name;
                manager.currentmelee = armas.rightEquip.name;
                #endregion

                #region Disable things
                getter.Pausemanager.SetActive(false);
                camara.GetComponent<Throwinggrenade>().enabled = false;
                reticle.SetActive(false);
                camara.GetComponent<Camara>().enabled = false;
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
}
