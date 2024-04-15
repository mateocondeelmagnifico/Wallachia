using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject Door;
    public int doorNumber;
    MoveObject movement;
    bool Islocked;
    public bool isAutomatic;
    RespawnManager resManager;
    void Start()
    {
        movement = Door.GetComponent<MoveObject>();
        resManager = FindObjectOfType<RespawnManager>();
        Islocked = resManager.lockedDoors[doorNumber];
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
            FindObjectOfType<RespawnManager>().RememberDoorState(doorNumber);
        }
    }
}
