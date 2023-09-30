using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerPlayer : MonoBehaviour
{
    movement movimiento;

    private void Start()
    {
        movimiento = GetComponent<movement>();
    }

    void Update()
    {
        MovementInputs();
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
}
