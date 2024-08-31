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

                armas.SetiInactive();
                armas.equippoint();

            }
        }
    }
}
