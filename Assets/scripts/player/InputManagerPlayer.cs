using UnityEngine;
using WeaponMechanics;

namespace PlayerMechanics
{
    public class InputManagerPlayer : MonoBehaviour
    {
        movement movimiento;
        public GameObject crossObj;
        Cross cruz;
        private weapons weaponsScript;

        private float crossCooldown;

        public bool hasCross, hasBullets;
        private void Start()
        {
            movimiento = GetComponent<movement>();
            cruz = crossObj.GetComponentInChildren<Cross>();
            weaponsScript = GetComponent<weapons>();
        }

        void Update()
        {
            if (crossCooldown > 0)
            {
                crossCooldown -= Time.deltaTime;
            }
            MovementInputs();
            CrossInput();
            OtherInputs();
        }

        private void MovementInputs()
        {
            movimiento.zInput = 0;
            movimiento.xInput = 0;
            if (Input.GetKey(KeyCode.W))
            {
                movimiento.zInput = 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                movimiento.zInput = -1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movimiento.xInput = 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movimiento.xInput = -1;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movimiento.shiftInput = 1;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                movimiento.shiftInput = -1;
            }
        }
        private void CrossInput()
        {
            if (!hasCross) return;

            if (Input.GetKey(KeyCode.F) && crossCooldown <= 0)
            {
                cruz.Activate();
                crossCooldown = 9;
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                cruz.Deactivate();
            }
        }
        private void OtherInputs()
        {
            if(Input.GetKeyDown(KeyCode.Q) && hasBullets)
            {
                weaponsScript.ChangeBullet();
            }
        }
    }
}
