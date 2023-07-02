using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject Door;
    MoveObject movement;
    void Start()
    {
        movement = Door.GetComponent<MoveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if (movement.ismoving == false)
            {
                movement.Move();
            }
        }
    }
}
