using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookatplayer : MonoBehaviour
{
    public Transform player;
    Vector3 startingposition;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        startingposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        direction.y = 0;
        transform.forward = direction;
    }
}
