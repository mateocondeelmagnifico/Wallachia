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

    public Animator animador;

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
        animador.SetBool("Moving", false);
        direction = new Vector3(0, 0, 0);
        xdirection = new Vector3(0, 0, 0);
        zdirection = new Vector3(0, 0, 0);
        if (canmove == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                zdirection = transform.forward;
                animador.SetBool("Moving", true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //You go back more slowly to discourage retreating
                zdirection = -transform.forward;
                animador.SetBool("Moving", true);
                speed = 2f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                xdirection = transform.right;
                animador.SetBool("Moving", true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                xdirection = -transform.right;
                animador.SetBool("Moving", true);
            }
            direction = xdirection + zdirection;
            direction = direction.normalized;
        }
    }
}
