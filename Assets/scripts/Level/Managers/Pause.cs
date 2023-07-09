using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    //Pause sets timescale to 0 and sets the bool ispaused to true in all the scripts that can still work with timescale = 0

    public bool[] istrue;
    public bool ispaused;
    public bool isonmenu;

    public GameObjectgetter getter;
    public GameObject[] exceptions;
    public GameObject controlsmenu;

    Sonido sound;

    //Exception 1 = pistola, Excepcion 2 = rifle, Excepcion 3 = player, Excepcion 4 = camara, Exception 5 = soundmanager,
    //Exception 6 = game paused menu, Exception 7 = reloading text, Exception 8 = top right images
    void Start()
    {
        exceptions[0] = getter.gun1;
        exceptions[1] = getter.gun2;
        exceptions[2] = getter.Player;
        exceptions[3] = getter.cam;
        exceptions[4] = getter.Soundmanager;
        exceptions[5] = getter.gamepausedmenu;
        exceptions[6] = getter.reloadingtext;
        exceptions[7] = getter.Toplefticons;

        sound = exceptions[4].GetComponent<Sonido>();
        controlsmenu = getter.controlsmenu;
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
                exceptions[7].SetActive(true);
                controlsmenu.SetActive(false);
                isonmenu = false;
            }
        }
    }
    public void unpause()
    {
        exceptions[0].GetComponent<Shooting>().ispaused = false;
        exceptions[1].GetComponent<Shooting>().ispaused = false;
        exceptions[2].GetComponent<Attack>().ispaused = false;
        exceptions[3].GetComponent<Throwinggrenade>().ispaused = false;
        exceptions[5].SetActive(false);
        sound.sources[2].volume = 0.4f;

        if (istrue[0] == true)
        {
            sound.sources[1].Play();
        }

        if (istrue[1] == true)
        {
            exceptions[6].GetComponent<Text>().enabled = true;
        }
      
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
    }
    public void pause(string howmuch)
    {
        exceptions[0].GetComponent<Shooting>().ispaused = true;
        exceptions[1].GetComponent<Shooting>().ispaused = true;
        exceptions[2].GetComponent<Attack>().ispaused = true;
        exceptions[3].GetComponent<Throwinggrenade>().ispaused = true;
        sound.sources[2].volume = 0.15f;

        if (sound.sources[1].isPlaying == true)
        {
            istrue[0] = true;
            sound.sources[1].Pause();
        }
        else
        {
            istrue[0] = false;
        }

        if (exceptions[6].GetComponent<Text>().enabled == true) 
        {
            istrue[1] = true;
            exceptions[6].GetComponent<Text>().enabled = false;
        }
        else
        {

            istrue[1] = false;
        }

    
        if (howmuch == "Full")
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            exceptions[5].SetActive(true);
        }
      

        Time.timeScale = 0;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void showcontrols()
    {
        exceptions[7].SetActive(false);
        controlsmenu.SetActive(true);
        isonmenu = true;
    }
}
