using UnityEngine;
using WeaponMechanics;

public class RespawnManager : MonoBehaviour
{
    public weapons playerweapons;

    public Vector3 spawnposition;

    public bool hasGrenades;
    public bool hasAxe;
    public bool hasBullets;
    public bool hasRifle;
    public bool hasSword;

    [HideInInspector]
    public bool[] lockedDoors;

    public int currentgrenade;
    public int currentgun;
    public int currentmelee;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        lockedDoors = new bool[20];
    }

    // Update is called once per frame
    void Update()
    {
        #region Check Which weapons you have unlocked
        if (playerweapons.hasgrenade)
        {
            hasGrenades = true;
        }
        if (playerweapons.hasbullet)
        {
            hasBullets = true;
        }
        if (playerweapons.hasaxe)
        {
            hasAxe = true;
        }
        if (playerweapons.hasrifle)
        {
            hasRifle = true;
        }
        if (playerweapons.hassword)
        {
            hasSword = true;
        }
        #endregion
    }

    //This bool is to keep track of which doors should be closed in case of a restart
    public void RememberDoorState(int whichdoor)
    {
        lockedDoors[whichdoor] = true;
    }

}
