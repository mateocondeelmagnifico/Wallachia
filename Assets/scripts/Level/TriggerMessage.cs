using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMessage : MonoBehaviour
{
    [Header("The name must match with the name in DisplayText")]
    public string whatMessage;
    [Header("Select what key you have to press to hide the message")]
    public KeyCode whatKey;
    public DisplayText texto;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(whatKey))
        {
            texto.HideText();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        texto.ShowText(whatMessage);
    }
}
