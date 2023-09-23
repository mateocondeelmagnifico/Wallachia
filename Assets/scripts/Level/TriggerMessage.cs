using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMessage : MonoBehaviour
{
    [Header("Press if you want to use timer, if not end with key press")]
    public bool usingTimer;
    public KeyCode whatKey;
    public float timer;

    [Header("This is for a message")]
    [TextArea]
    public string whatMessage;

    [Header("This is you want it to show an icon")]
    public Sprite image;

    public DisplayText texto;

    bool isEnabled;


    void Update()
    {
        //This is to make the text dissapear if you press a key
        if(Input.GetKeyDown(whatKey) && isEnabled == true && !usingTimer)
        {
            texto.HideTextAndImage();
            isEnabled = false;
        }

        //This makes text dissapear after some time
  
        if (isEnabled && usingTimer)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                texto.HideTextAndImage();
                isEnabled = false;
                GetComponent<Collider>().enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (whatMessage != null)
            {
                texto.ShowText(whatMessage);
            }
            
            if (image != null)
            {
                texto.showImage(image);
            }

            isEnabled = true;
        }
    }
}
