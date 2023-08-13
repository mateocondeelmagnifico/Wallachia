using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    Text texto;
    void Start()
    {
        texto = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowText(string whattext)
    {
        texto.text = whattext;
    }
    public void HideText()
    {
        texto.text = "";
    }
}
