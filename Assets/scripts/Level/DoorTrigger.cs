using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject Door;
    MoveObject movement;
    bool Islocked;
    void Start()
    {
        movement = Door.GetComponent<MoveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.ismoving == false && Islocked)
        {
            movement.Move();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Islocked = true;
        }
    }
}
