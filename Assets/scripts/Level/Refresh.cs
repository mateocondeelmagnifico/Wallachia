using UnityEngine;
using PlayerMechanics;
using WeaponMechanics;

public class Refresh : MonoBehaviour
{
    public GameObjectgetter getter;

    Sonido sound;
    RespawnManager spawnmanager;
    Throwinggrenade grenadescript;
    // Refills HP and Ammo
    void Start()
    {
        grenadescript = getter.cam.GetComponent<Throwinggrenade>();
        sound = getter.Soundmanager.GetComponent<Sonido>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.GetComponent<Life>().health = 6;
            collision.GetComponent<Life>().riflereloaded = false;
            collision.GetComponent<Life>().pistolreloaded = false;
            grenadescript.remaininggarlic = 2;
            grenadescript.remainingsilver = 2;

            sound.playaudio("Checkpoint");

            spawnmanager = FindObjectOfType<RespawnManager>();
            spawnmanager.spawnposition = transform.position;
            Destroy(this.gameObject);
        }
    }
}
