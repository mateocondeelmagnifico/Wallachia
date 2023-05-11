using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // This is for the player movement

    public CharacterController controlador;

    public Vector3 direction;

    public Vector3 xdirection;
    public Vector3 zdirection;


    public bool canmove;

    float speed;
    void Start()
    {
        canmove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Keycheck();
        if (controlador.isGrounded == false)
        {
            direction.y = -3f;
        }
        controlador.Move(direction * speed * Time.deltaTime);
    }
    public void Keycheck()
    {
        speed = 5;
        direction = new Vector3(0, 0, 0);
        xdirection = new Vector3(0, 0, 0);
        zdirection = new Vector3(0, 0, 0);
        if (canmove == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                zdirection = transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                //You go back more slowly to discourage retreating
                zdirection = -transform.forward;
                speed = 2f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                xdirection = transform.right;
            }
            if (Input.GetKey(KeyCode.A))
            {
                xdirection = -transform.right;
            }
            direction = xdirection + zdirection;
            direction = direction.normalized;
        }
    }
}
