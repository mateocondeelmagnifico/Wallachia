using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public GameObject parent;
    public Image imagen;
    void Start()
    {
        imagen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imagen.fillAmount = parent.GetComponent<Enemy>().life / parent.GetComponent<Enemy>().maxlife;
        transform.LookAt(GameObject.Find("Player").transform.position);
    }
}
