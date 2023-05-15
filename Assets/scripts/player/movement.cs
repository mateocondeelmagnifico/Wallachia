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

    Image stamina;

    public Vector3 direction;
    public Vector3 xdirection;
    public Vector3 zdirection;


    public bool canmove;
    public bool issprinting;
    public bool isonhill;

    public float speed;
    public float cansprint;
    void Start()
    {
        canmove = true;
        cansprint = 4;
        gunscripts[0] = guns[0].GetComponent<Shooting>();
        gunscripts[1] = guns[1].GetComponent<Shooting>();
        stamina = staminabar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        stamina.fillAmount = cansprint/4;
        Keycheck();
        if (controlador.isGrounded == false)
        {
            direction.y = -3f;
        }


        if (issprinting == true || cansprint < 4)
        {
            stamina.color = new Color(1, 1, 1, 1);
        }
        else
        {
            stamina.color = new Color(1, 1, 1, 0);
        }

        if (cansprint < 4 && issprinting == false)
        {
            cansprint += Time.deltaTime/ 3.5f;
            if (cansprint < 2)
            {
                speed /= 2;
            }
        }
        if (isonhill == true)
        {
            speed /= 2;
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
            //Sprinting
            if (Input.GetKey(KeyCode.LeftShift) && cansprint > 0)
            {
                issprinting = true; 
                speed *= 1.9f;
                Setrunning(true);
                cansprint -= Time.deltaTime * 1.3f;
            }
            if ((Input.GetKeyUp(KeyCode.LeftShift)))
            {
                Setrunning(false);
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

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Hill"))
        {
            isonhill = true;
        }      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Hill"))
        {
            isonhill = false;
        }
    }
}
