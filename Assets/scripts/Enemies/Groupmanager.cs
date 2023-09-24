using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Groupmanager : MonoBehaviour
{
    //Enemies come in groups, this script makes it so that they all attack the player if one is alerted
    //It also drags the player and hitmarker gameobject to each enemy, so i dont have to drag it to each individual enemy
    // remember dragging the enemies to the group manager
    public string enemytype;
    public bool activated;
    public bool isattacking;
    public int currentball;

    public GameObject player;
    public GameObject hitmarker;
    Image Hitmarker;

    void Start()
    {
        Hitmarker = hitmarker.GetComponent<Image>();
        //This is so that all enemies in the stage get this things
        for (int count = transform.childCount; count > 0; count--)
        {
            transform.GetChild(count - 1).GetComponent<BasicEnemy>().hitmarkerObject = hitmarker;
            transform.GetChild(count - 1).GetComponent<BasicEnemy>().hitmarker = Hitmarker;

            transform.GetChild(count - 1).GetComponent<BasicEnemyMovement>().player = player;
            transform.GetChild(count - 1).GetComponent<BasicEnemyMovement>().groupManager = this;
        }
    }

    //This is to prevent enemies from straying too far
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.GetComponent<BasicEnemyMovement>().returnHome(transform.position);
        }
    }
}
