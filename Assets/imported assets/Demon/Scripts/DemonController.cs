using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// demo setup 

public class DemonController : MonoBehaviour
{
    private Animator _anim;
    private bool _walk;
    [SerializeField]
    private float _walkingSpeed;

    void Start()
    {
        _anim = GetComponent<Animator>();

         StartCoroutine("StartWalk"); 
    }


    void Update()
    {
        if(_walk)
            Move();

    }

    IEnumerator StartWalk()
    {
        yield return new WaitForSeconds(2.0f);
        _walk = true;
        _anim.SetTrigger("walk");
        yield return new WaitForSeconds(2.5f);
        _walk = false;
        _anim.SetTrigger("attak1");
        yield return new WaitForSeconds(1.2f);
        _anim.SetTrigger("attak2");
        yield return new WaitForSeconds(1.5f);
        _anim.SetTrigger("die");
    }

    private void Move()
    {
            transform.Translate(Vector3.forward * Time.deltaTime * _walkingSpeed, Space.World);
    }
}
