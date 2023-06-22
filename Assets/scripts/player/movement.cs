using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    // This is for the player movement

    public CharacterController controlador;

    public GameObject[] guns;
    public Shooting[] gunscripts;
    public GameObject staminabar;

    //public Image stamina;

    public Vector3 direction;
    public Vector3 xdirection;
    public Vector3 zdirection;


    public bool canmove;
    public bool issprinting;

    public float speed;
    public float sprinttimer;
    void Start()
    {
        canmove = true;
        gunscripts[0] = guns[0].GetComponent<Shooting>();
        gunscripts[1] = guns[1].GetComponent<Shooting>();
        //stamina = staminabar.GetComponent<Image>();
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
            if (sprinttimer > 0)
            {
                sprinttimer -= Time.deltaTime;
            }
            else
            {
                Setrunning(false);
            }
        }
        //stamina.fillAmount = cansprint/4;
        Keycheck();
        if (controlador.isGrounded == false)
        {
            direction.y = -3f;
        }
        controlador.Move(direction * speed * Time.deltaTime);
    }
    public void Keycheck()
    {
        speed = 6;
        direction = new Vector3(0, 0, 0);
        xdirection = new Vector3(0, 0, 0);
        zdirection = new Vector3(0, 0, 0);
        if (canmove == true)
        {
            //Sprinting
            if (Input.GetKey(KeyCode.LeftShift))
            {
                issprinting = true;
                if (sprinttimer >= 0.3f)
                {
                    speed *= 1.6f;
                    Setrunning(true);
                }
            }

            if ((Input.GetKeyUp(KeyCode.LeftShift)))
            {
                issprinting = false;
             
            }

            if (Input.GetKey(KeyCode.W))
            {
                zdirection = transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                //You go back more slowly to discourage retreating
                zdirection = -transform.forward;
                speed /= 2;
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
    public void Setrunning(bool istrue)
    {
      if (istrue == true)
        {
            gunscripts[0].isrunning = true;
            gunscripts[1].isrunning = true;
        }
        else
        {
            gunscripts[0].isrunning = false;
            gunscripts[1].isrunning = false;
        }
    }
}
