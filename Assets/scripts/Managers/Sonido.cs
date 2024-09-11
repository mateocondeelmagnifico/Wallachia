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

    public bool isPlayer;

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

            if (isPlayer)
            {
                playaudio("Ambient Music");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isPlayer)
        {
            sources[1].volume -= Time.deltaTime / 8;
        }
    }
    public void playaudio(string type)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (type == sounds[i].name)
            {

                currentsource = sources[sounds[i].source];

                currentsource.clip = sounds[i].clip;
                currentsource.volume = sounds[i].volume;
                currentsource.Play();
                break;
            }
        }
    }
}
