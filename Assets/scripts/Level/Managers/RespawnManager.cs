using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    weapons playerweapons;

    public Vector3 spawnposition;

    public bool hasgrenades;
    public bool hasaxe;
    public bool hasbullets;
    public bool hasrifle;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        playerweapons = GameObject.FindObjectOfType<weapons>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerweapons.hasgrenade)
        {
            hasgrenades = true;
        }
        if (playerweapons.hasbullet)
        {
            hasbullets = true;
        }
        if (playerweapons.hasaxe)
        {
            hasaxe = true;
        }
        if (playerweapons.hasrifle)
        {
            hasrifle = true;
        }
    }
}
