using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    // This is for the player movement

    public CharacterController controlador;

    public GameObject Getter;
    GameObject[] guns;
    public Shooting[] gunscripts;
    Animator animador;


    public Vector3 direction;
    public Vector3 xdirection;
    public Vector3 zdirection;


    public bool canmove;
    public bool issprinting;

    public int zInput;
    public int xInput;
    public int shiftInput;

    public float speed;
    public float sprinttimer;
    void Start()
    {
        animador = GetComponent<Animator>();
        canmove = true;
        gunscripts[0] = Getter.GetComponent<GameObjectgetter>().gun1.GetComponent<Shooting>();
        gunscripts[1] = Getter.GetComponent<GameObjectgetter>().gun2.GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (issprinting)
        {
            if (sprinttimer < 0.3f)
            {
                sprinttimer += Time.deltaTime;
            }
        }
        else
        {
            gunscripts[0].keypressed = false;
            gunscripts[1].keypressed = false;
            if (sprinttimer > 0)
            {
                sprinttimer -= Time.deltaTime;
            }
            else
            {
                Setrunning(false);
            }
        }

        Keycheck();
        if (controlador.isGrounded == false)
        {
            direction.y = -3f;
        }
        controlador.Move(direction * speed * Time.deltaTime);

        #region Walking Animation
        animador.SetFloat("WalkSpeed", speed / 5);
        if (xdirection != Vector3.zero || zdirection != Vector3.zero)
        {
            animador.SetBool("Moving", true);
        }
        else
        {
            animador.SetBool("Moving", false);
        }
        #endregion
    }
    public void Keycheck()
    {
        //Input manager manages the key presses
        speed = 5;
        direction = new Vector3(0, 0, 0);
        xdirection = new Vector3(0, 0, 0);
        zdirection = new Vector3(0, 0, 0);
        if (canmove == true)
        {
            //Sprinting
            if (shiftInput == 1)
            {
                issprinting = true;

               speed *= 1.15f + sprinttimer;
               Setrunning(true);
            }
            else
            {
                issprinting = false;
            }

            if (zInput == 1)
            {
                zdirection = transform.forward;
            }
            if (zInput == -1)
            {
                //You go back more slowly to discourage retreating
                zdirection = -transform.forward;
                speed /= 2;
            }
            if (xInput == 1)
            {
                xdirection = transform.right;
            }
            if (xInput == -1)
            {
                xdirection = -transform.right;
            }

           
            direction = xdirection + zdirection;
            direction = direction.normalized;
        }
    }
    public void Setrunning(bool istrue)
    {
      if (istrue == true)
        {
            gunscripts[0].isrunning = true;
            gunscripts[1].isrunning = true;
            gunscripts[0].keypressed = true;
            gunscripts[1].keypressed = true;

        }
        else
        {
            gunscripts[0].isrunning = false;
            gunscripts[1].isrunning = false;
        }
    }
}
