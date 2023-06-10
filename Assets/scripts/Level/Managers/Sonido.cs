using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    //Soundmanager script, it is called by different objects in the scene
    AudioSource source1;
    AudioSource source2;
    public Soundsclass[] sounds;

    bool lowering;
    void Start()
    {
        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

            source2.volume -= Time.deltaTime / 8;
        
    }
    public void playaudio(string type)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (type == sounds[i].name)
            {
                AudioSource source;
                if (sounds[i].source == 0)
                {
                    source = source1;
                }
                else
                {
                    source = source2;
                }
                source.clip = sounds[i].clip;
                source.volume = sounds[i].volume;
                source.Play();
                break;
            }
        }
    }
}
