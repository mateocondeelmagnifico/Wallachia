using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGetter : MonoBehaviour
{
    public GameObject respawnmanager;
    void Awake()
    {
        if (FindObjectOfType<RespawnManager>() == null)
        {
            Instantiate(respawnmanager,transform.position,transform.rotation);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
