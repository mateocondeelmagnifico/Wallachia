using UnityEngine;
using UnityEngine.UI;
using PlayerMechanics;
using WeaponMechanics;

public class Pause : MonoBehaviour
{
    //Pause sets timescale to 0 and sets the bool ispaused to true in all the scripts that can still work with timescale = 0

    public bool[] istrue;
    public bool ispaused;
    public bool isonmenu;
    bool isOnWeaponWheel;

    public GameObjectgetter getter;
    public GameObject[] exceptions;

    Sonido sound;
    MenuManager menuManager;

    //Exception 1 = pistola, Excepcion 2 = rifle, Excepcion 3 = player, Excepcion 4 = camara, Exception 5 = soundmanager,
    //Exception 6 = game paused menu, Exception 7 = reloading text, Exception 8 = top right images
    void Start()
    {
        exceptions[0] = getter.gun1;
        exceptions[1] = getter.gun2;
        exceptions[2] = getter.Player;
        exceptions[3] = getter.cam;
        exceptions[4] = getter.Soundmanager;
        exceptions[5] = getter.reloadingImage;
        exceptions[6] = getter.Toplefticons;
        exceptions[7] = getter.textDisplay;
        exceptions[8] = getter.reticle;

        menuManager = getter.MenuManager.GetComponent<MenuManager>();
        sound = exceptions[4].GetComponent<Sonido>();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            ispaused = true;
        }
        else
        {
            ispaused = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isonmenu == false)
            {
                if (ispaused == false)
                {
                    pause("Full");
                }
                else
                {
                    unpause();
                }
            }
            else
            {
                exceptions[6].SetActive(true);
                unpause();
                isonmenu = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!ispaused)
            {
                pause("weapon wheel");
                isOnWeaponWheel = true;
            }
            else
            {
                if(isOnWeaponWheel)
                {
                    isOnWeaponWheel = false;
                    unpause();
                }
            }
        }
    }
    public void unpause()
    {
        #region things that are unpaused
        exceptions[0].GetComponent<Shooting>().ispaused = false;
        exceptions[1].GetComponent<Shooting>().ispaused = false;
        exceptions[2].GetComponent<Attack>().ispaused = false;
        exceptions[2].GetComponent<weapons>().checkUnpause();
        exceptions[3].GetComponent<Throwinggrenade>().ispaused = false;
        exceptions[7].GetComponent<Text>().enabled = true;
        exceptions[8].GetComponent<Image>().enabled = true;
        sound.sources[2].volume = 0.4f;
        #endregion

        if (istrue[0] == true)
        {
            sound.sources[1].Play();
        }

        if (istrue[1] == true)
        {
            exceptions[5].GetComponent<Image>().enabled = true;
        }


        menuManager.changemenu("none");
      
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
    }
    public void pause(string whichPause)
    {
        #region thigs that are paused
        sound.playaudio("Menu Enter", null);
        exceptions[0].GetComponent<Shooting>().ispaused = true;
        exceptions[1].GetComponent<Shooting>().ispaused = true;
        exceptions[2].GetComponent<Attack>().ispaused = true;
        exceptions[3].GetComponent<Throwinggrenade>().ispaused = true;
        exceptions[7].GetComponent<Text>().enabled = false;
        exceptions[8].GetComponent<Image>().enabled = false;
        sound.sources[2].volume = 0.15f;
        #endregion

        if (sound.sources[1].isPlaying == true)
        {
            istrue[0] = true;
            sound.sources[1].Pause();
        }
        else
        {
            istrue[0] = false;
        }

        if (exceptions[5].GetComponent<Image>().enabled == true) 
        {
            istrue[1] = true;
            exceptions[5].GetComponent<Image>().enabled = false;
        }
        else
        {
            istrue[1] = false;
        }
        
    
        if (whichPause == "Full")
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            menuManager.changemenu("pause");
        }

        if (whichPause == "weapon wheel")
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            menuManager.changemenu("weapon wheel");
        }

        Time.timeScale = 0;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void showcontrols()
    {
        exceptions[6].SetActive(false);
        menuManager.changemenu("controls");
        isonmenu = true;
    }
}
