using UnityEngine;
using WeaponMechanics;

namespace PlayerMechanics
{
    public class SpawnValues : MonoBehaviour
    {
        public GameObject player;
        weapons armas;
        public RespawnManager manager;

        void Awake()
        {
            armas = GetComponent<weapons>();
            if (RespawnManager.instance != null)
            {
                manager = RespawnManager.instance;

                if (manager.spawnposition != Vector3.zero)
                {
                    player.transform.position = manager.spawnposition;
                }

                armas.hasaxe = manager.hasAxe;
                armas.hasrifle = manager.hasRifle;
                armas.hasBullet = manager.hasBullets;
                armas.hasgrenade = manager.hasGrenades;
                armas.hassword = manager.hasSword;
                armas.currentEquip[2] = manager.currentgrenade;
                armas.currentEquip[0] = manager.currentmelee;
                armas.currentEquip[1] = manager.currentgun;

                armas.SetiInactive();
                armas.equippoint();

            }
        }
    }
}
