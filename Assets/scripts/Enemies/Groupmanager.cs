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

    //public int currentball;
    void Start()
    {
        Hitmarker = hitmarker.GetComponent<Image>();
        for (int count = transform.childCount; count > 0; count--)
        {
            transform.GetChild(count - 1).GetComponent<Enemy>().hitmarker = Hitmarker;
            transform.GetChild(count - 1).GetComponent<Enemymovement>().player = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
