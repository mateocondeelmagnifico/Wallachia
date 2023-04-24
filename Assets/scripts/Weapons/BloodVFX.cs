using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVFX : MonoBehaviour
{
    public Vector3 orientation;
    float destroytimer;
    void Start()
    {
        destroytimer = 0.25f;
        transform.forward = orientation;
    }

    // Update is called once per frame
    void Update()
    {
        destroytimer -= Time.deltaTime;
        if (destroytimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
