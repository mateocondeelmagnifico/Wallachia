using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
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
        controlador.Move(direction * speed * Time.deltaTime);
        if (transform.position.y > 7.81f || transform.position.y < 7.81f)
        {
            transform.position = new Vector3(transform.position.x, 7.81f, transform.position.z);
        }
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
