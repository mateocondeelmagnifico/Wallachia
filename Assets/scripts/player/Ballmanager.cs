using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballmanager : MonoBehaviour
{
    public GameObject follower;
    public GameObject[] followers;
    public int currentball;
    void Start()
    {
        //currentball = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void createball()
    {
        followers[currentball] = Instantiate(follower);
    }
}
