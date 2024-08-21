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
                armas.tempGrenade = manager.currentgrenade;
                armas.tempRightEquip = manager.currentmelee;
                armas.tempEquipLeft = manager.currentgun;

                armas.SetiInactive();
                armas.equippoint();

            }
        }
    }
}
