using System.Collections.Generic;
using UnityEngine;
using WeaponMechanics;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    public weapons playerweapons;

    public Vector3 spawnposition;

    public List<string> unlockedThings = new List<string>();

    [HideInInspector]
    public bool[] lockedDoors;

    public string currentgun, currentmelee, currentgrenade;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
        lockedDoors = new bool[20];
    }

    //This bool is to keep track of which doors should be closed in case of a restart
    public void RememberDoorState(int whichdoor)
    {
        lockedDoors[whichdoor] = true;
    }

}
