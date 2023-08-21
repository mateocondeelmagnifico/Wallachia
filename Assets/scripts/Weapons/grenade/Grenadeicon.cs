using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grenadeicon : MonoBehaviour
{
    public Sprite[] icons;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().sprite = icons[GameObject.Find("Player").GetComponent<weapons>().currentEquip[2]];
    }
}
