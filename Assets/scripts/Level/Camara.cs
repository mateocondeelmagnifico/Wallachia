using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    //FPS Camera script

    public float xsensitivity;
    public float ysensitivity;

    public Transform defaultaim;
    public Transform orientation;

    //public GameObject aimpoint;
    public GameObject player;

    float xrotation;
    float yrotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * Time.deltaTime * xsensitivity;
        float mousey = Input.GetAxis("Mouse Y") * Time.deltaTime * ysensitivity;

        yrotation += mousex;
        xrotation -= mousey;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xrotation, transform.rotation.eulerAngles.y, 0);
        orientation.rotation = Quaternion.Euler(0, yrotation,0);

        
        RaycastHit hit;
        LayerMask mascara = LayerMask.GetMask("Enemies");

        Ray rayo = new Ray(transform.position, transform.forward);

        //The gun aims towards the aimpoint gameobject, this script uses a raycast to place the aimpoint gameobject at the end of it
        if (Physics.Raycast(rayo, out hit, 2000,mascara))
        {
            //aimpoint.transform.position = hit.point;
            //player.GetComponent<Attack>().tooclose = false;
        }
        else
        {
            //aimpoint.transform.position = defaultaim.position;
        }
        //if (Vector3.Distance(transform.position, aimpoint.transform.position) < 2)
        //{
        //if the enemy is too close then the gun just aims forward, to avoid bugs
        // player.GetComponent<Attack>().tooclose = true;
        //aimpoint.transform.position = defaultaim.position;
    }
}

