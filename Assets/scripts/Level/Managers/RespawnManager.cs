using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Vector3 spawnposition;

    public bool hasgrenades;
    public bool hasaxe;
    public bool hasbullets;
    public bool hasrifle;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
