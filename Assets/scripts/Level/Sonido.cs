using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    //Soundmanager script, it is called by different objects in the scene
    AudioSource source;
    public AudioClip[] clip;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playaudio(string type)
    {
        if (type == "shoot")
        {
            source.volume = 0.9f;
            source.clip = clip[0];
            source.Play();
        }
        
        if (type == "dont shoot")
        {
            source.volume = 0.3f;
            source.clip = clip[1];
            source.Play();
        }

        if (type == "Pickup")
        {
            source.volume = 0.4f;
            source.clip = clip[2];
            source.Play();
        }

        if (type == "Garlic Grenade")
        {
            source.volume = 0.8f;
            source.clip = clip[3];
            source.Play();
        }
    }
}
