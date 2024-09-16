using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Sonido sound;

    public float xdistance;
    public float ydistance;
    public float zdistance;
    public float speed;

    float movex;
    float movey;
    float movez;

    public bool ismoving;
    bool isinplace;

    // Update is called once per frame
    void Update()
    {
        if (ismoving && isinplace == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (movex + xdistance, movey + ydistance, movez + zdistance), speed * Time.deltaTime);
        }

    }

    public void Move()
    {
        sound.playaudio("Gate Closing", null);
        ismoving = true;
        movex = transform.position.x;
        movey = transform.position.y;
        movez = transform.position.z;
    }
}
