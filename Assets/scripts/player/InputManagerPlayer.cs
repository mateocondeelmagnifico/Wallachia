using UnityEngine;
using WeaponMechanics;

namespace PlayerMechanics
{
    public class InputManagerPlayer : MonoBehaviour
    {
        movement movimiento;
        public GameObject crossObj;
        Cross cruz;

        private float crossCooldown;
        private void Start()
        {
            movimiento = GetComponent<movement>();
            cruz = crossObj.GetComponentInChildren<Cross>();
        }

        void Update()
        {
            if (crossCooldown > 0)
            {
                crossCooldown -= Time.deltaTime;
            }
            MovementInputs();
            CrossInput();
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
            if (Input.GetKey(KeyCode.Q) && crossCooldown <= 0)
            {
                cruz.Activate();
                crossCooldown = 1;
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                cruz.Deactivate();
            }
        }
    }
}
