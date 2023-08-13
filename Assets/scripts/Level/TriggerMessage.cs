using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMessage : MonoBehaviour
{
    [Header("Type message here")]
    [TextArea]
    public string whatMessage;

    [Header("Select what key you have to press to hide the message")]
    public KeyCode whatKey;

    [Header("Activate timer if you don't want to use keycodes")]
    public bool useTimer;
    public float timer;
    float timerStartValue;
    public DisplayText texto;

    bool isEnabled;

    void Start()
    {
        timerStartValue = timer;
    }

    // Update is called once per frame
    void Update()
    {
        //This is to make the text dissapear if you press a key
        if(Input.GetKeyDown(whatKey) && useTimer == false && isEnabled == true)
        {
            texto.HideText();
            isEnabled = false;
        }

        //This makes it dissapear with a timer
        if (useTimer && isEnabled)
        {
            if(timer >= 0)
            {
                timer -= Time.deltaTime;
            }
           else
            {
                texto.HideText();
                isEnabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            texto.ShowText(whatMessage);
            isEnabled = true;
            timer = timerStartValue;
        }
    }
}
