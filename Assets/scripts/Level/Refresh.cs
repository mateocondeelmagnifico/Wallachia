using UnityEngine;
using PlayerMechanics;
using WeaponMechanics;

public class Refresh : LevelInteractable
{
    Sonido sound;
    RespawnManager spawnmanager;
    Throwinggrenade grenadescript;
    // Refills HP and Ammo
    void Start()
    {
        //grenadescript = getter.cam.GetComponent<Throwinggrenade>();
        sound = Sonido.instance;
    }

    public override void Interact(GameObject player)
    {
        Life playerLife = player.GetComponent<Life>();

        playerLife.ChangeLife(6);
        playerLife.riflereloaded = false;
        playerLife.pistolreloaded = false;
        //grenadescript.remaininggarlic = 2;
        //grenadescript.remainingsilver = 2;

        sound.playaudio("Checkpoint");

        spawnmanager = FindObjectOfType<RespawnManager>();
        spawnmanager.spawnposition = transform.position;
        Destroy(this.gameObject);
    }

}
