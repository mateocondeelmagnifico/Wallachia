using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Sonido : MonoBehaviour
{
    //Soundmanager script, it is called by different objects in the scene

    public static Sonido instance { get; private set; }

    public Soundsclass[] sounds;
    public int numberOfSources;

    //[HideInInspector]
    public AudioSource[] sources = new AudioSource[4];

    AudioSource currentsource;

    bool lowering;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            for (int i = 0; i < numberOfSources; i++)
            {
                sources[i] = GetComponents<AudioSource>()[i];
            }

            playaudio("Ambient Music", null);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //This source decreasses in volume over time
        if (sources[1].volume > 0)   sources[1].volume -= Time.deltaTime / 8;
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
}
