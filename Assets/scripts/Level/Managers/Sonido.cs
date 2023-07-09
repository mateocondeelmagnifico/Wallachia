using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Sonido : MonoBehaviour
{
    //Soundmanager script, it is called by different objects in the scene

    public Soundsclass[] sounds;

    [HideInInspector]
    public AudioSource[] sources = new AudioSource[4];

    AudioSource currentsource;

    public bool isenemy;

    bool lowering;
    void Start()
    {
        sources[0] = gameObject.AddComponent<AudioSource>();
        sources[1] = gameObject.AddComponent<AudioSource>();
        sources[2] = gameObject.AddComponent<AudioSource>();
        if (isenemy)
        {
            sources[0].maxDistance = 1;
            sources[0].spatialBlend = 1;
            sources[1].maxDistance = 1;
            sources[1].spatialBlend = 1;
            sources[2].mute = true;
        }
        playaudio("Ambient Music");
    }

    // Update is called once per frame
    void Update()
    {

        sources[1].volume -= Time.deltaTime / 8;
        
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
