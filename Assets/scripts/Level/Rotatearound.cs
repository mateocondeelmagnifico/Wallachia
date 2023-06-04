using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatearound : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Rotatepoint;
    public float speed;
    void Start()
    {
        speed = 15;

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Rotatepoint.position, Vector3.up, speed * Time.deltaTime);
    }
}
