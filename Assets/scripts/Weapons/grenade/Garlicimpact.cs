using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlicimpact : MonoBehaviour
{
    public float timer;
    ParticleSystem particles;
    void Start()
    {
        particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        timer = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.6)
        {
            particles.Play();
        }
        timer -= Time.deltaTime;
        if (timer <= -3)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") && timer > 0)
        {
            other.GetComponent<Enemy>().takedamage(1, "Light");
            other.gameObject.GetComponent<Enemy>().statuseffect("Garlic");
        }
    }
}
