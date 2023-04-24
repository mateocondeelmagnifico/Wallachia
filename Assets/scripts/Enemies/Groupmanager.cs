using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Groupmanager : MonoBehaviour
{
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
