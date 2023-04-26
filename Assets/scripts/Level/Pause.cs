using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    //Pause sets timescale to 0 and sets the bool ispaused to true in all the scripts that can still work with timescale = 0

    public bool istrue;
    bool ispaused;
    public GameObject[] exceptions;

    //Exception 1 = pistola, Excepcion 2 = rifle, Excepcion 3 = player, Excepcion 4 = camara, Exception 5 = soundmanager
    void Start()
    {
        
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
            if (ispaused == false)
            {
                pause("Full");
            }
            else
            {
                unpause();
            }
        }
    }
    public void unpause()
    {
        exceptions[0].GetComponent<Shooting>().ispaused = false;
        exceptions[1].GetComponent<Shooting>().ispaused = false;
        exceptions[2].GetComponent<Attack>().ispaused = false;
        exceptions[3].GetComponent<Throwinggrenade>().ispaused = false;

        if (istrue == true)
        {
            exceptions[4].GetComponent<AudioSource>().Play();
        }

        exceptions[5].SetActive(false);
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

        if (exceptions[4].GetComponent<AudioSource>().isPlaying == true)
        {
            istrue = true;
            exceptions[4].GetComponent<AudioSource>().Pause();
        }
        else
        {
            istrue = false;
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
}
