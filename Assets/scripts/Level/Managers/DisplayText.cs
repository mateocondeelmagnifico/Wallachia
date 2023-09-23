using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    Text texto;
    public Image imagen;

    float timer;
    bool dissapear;

    void Start()
    {
        texto = GetComponent<Text>();
        imagen.color = new Color(1f, 1f, 1f,0);
    }

    void Update()
    {
        //Esto es para hacer desaparecer la imagen
        if (dissapear && timer > 0)
        {
            timer -= Time.deltaTime * 2;
        }
        imagen.color = new Color(1f, 1f, 1f, timer);
    }
    public void ShowText(string whattext)
    {
        texto.text = whattext;
    }
    public void showImage(Sprite image)
    {
        imagen.sprite = image;
        timer = 1;
        dissapear = false;
    }
    public void HideTextAndImage()
    {
        texto.text = "";
        dissapear = true;
    }
}
