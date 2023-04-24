using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool istrue;
    bool ispaused;
    public GameObject[] exceptions;

    //Exception 1 = pistola, Excepcion 2 = rifle, Excepcion 3 = player, Excepcion 4 = camara, Exception 5 = soundmanager
    void Start()
    {
        
    }

    // Update is called once per frame
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

                Time.timeScale = 0;
            }
            else
            {
                exceptions[0].GetComponent<Shooting>().ispaused = false;
                exceptions[1].GetComponent<Shooting>().ispaused = false;
                exceptions[2].GetComponent<Attack>().ispaused = false;
                exceptions[3].GetComponent<Throwinggrenade>().ispaused = false;

                if (istrue == true)
                {
                    exceptions[4].GetComponent<AudioSource>().Play();
                }
                Time.timeScale = 1;
            }
        }
    }
}
