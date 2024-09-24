using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Sonido : MonoBehaviour
{
    //Soundmanager script, it is called by different objects in the scene

    public static Sonido instance { get; private set; }

    public Soundsclass[] sounds;

    private bool introPlayed, transitioning1, transitioning2;

    public int numberOfSources;
    private float introTimer, transitionTimer, savedVolume;

    private string nextArea;

    //[HideInInspector]
    public AudioSource[] sources = new AudioSource[4];

    AudioSource currentsource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            for (int i = 0; i < numberOfSources; i++)
            {
                sources[i] = GetComponents<AudioSource>()[i];
            }
            playaudio("Intro", null);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        introTimer = sources[2].clip.length;
    }

    void Update()
    {
        //This source decreasses in volume over time
        if (sources[1].volume > 0)   sources[1].volume -= Time.deltaTime / 8;

        //Play intro before the ambient music at the start
        if(introTimer > 0) introTimer -= Time.deltaTime;
        else if(!introPlayed)
        {
            playaudio("Outside The Abbey", null);
            introPlayed = true;
        }

         #region Transition from one area to the next

        //Decrease volume to 0 and then raise
        if (transitioning1)
        {
            if(transitionTimer > 0)
            {
                transitionTimer -= Time.deltaTime;
                sources[2].volume = transitionTimer/8;
            }
            else
            {
                transitioning1 = false;
                transitioning2 = true;
                playaudio(nextArea, null);
                savedVolume = sources[2].volume;
                sources[2].volume = 0;
            }
        }

        if (transitioning2)
        {
            if (transitionTimer < 8)
            {
                transitionTimer += Time.deltaTime;
                if (sources[2].volume < savedVolume) sources[2].volume = transitionTimer / 8;
            }
            else transitioning2 = false;
        }
        #endregion
    }
    public void playaudio(string type, AudioSource source)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (type == sounds[i].name)
            {
                //Play sound with local sources or others

                if(source == null) currentsource = sources[sounds[i].source];
                else currentsource = source;

                currentsource.clip = sounds[i].clip;
                currentsource.volume = sounds[i].volume;
                currentsource.Play();
                break;
            }
        }
    }

    public void ChangeArea(int whichOne)
    {
        //Change the ambiant music in different areas

        switch(whichOne)
        {
            case 0:
                nextArea = "Outside The Abbey";
                break;

            case 1:
                nextArea = "Inside The Abbey";
                break;

            case 2:
                nextArea = "Seeing The Priest";
                break;

            case 3:
                nextArea = "The Caves";
                break;
        }

        transitionTimer = 8;
        transitioning1 = true;
    }
}
