using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Sonido : MonoBehaviour
{
    //Soundmanager script, it is called by different objects in the scene
    AudioSource source1;
    AudioSource source2;
    public Soundsclass[] sounds;

    public bool isenemy;

    bool lowering;
    void Start()
    {
        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();
        if (isenemy)
        {
            source1.maxDistance = 100;
            source1.spatialBlend = 1;
            source2.maxDistance = 100;
            source2.spatialBlend = 1;
        }
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
