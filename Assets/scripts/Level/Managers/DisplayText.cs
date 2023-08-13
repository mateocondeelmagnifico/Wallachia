using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    Text texto;

    string[] messages;

    int whatmessage;
    void Start()
    {
        texto = GetComponent<Text>();
        //In here are the messages you will get while playing
        #region Define messages
        messages = new string[8];
        messages[0] = "Press LEFT CLICK to shoot \n you deal more damage up close";

        messages[1] = "Press RIGHT CLICK to swing your sword \n You deal more damage if the enemy has less health";

        messages[2] = "Press F to throw grenades";

        messages[3] = "Press 4 to switch grenade types";

        messages[4] = "In this holy place, the air is of SILVER";

        messages[5] = "Press 2 to switch melee weapons";

        messages[6] = "Press 3 to switch to SILVER BULLETS";

        messages[7] = "Press 1 to switch to the RIFLE";
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowText(string whattext)
    {
        #region Check what message to show
        if (whattext == "Shooting")
        {
            whatmessage = 0;
        }
        if (whattext == "Melee Attack")
        {
            whatmessage = 1;
        }
        if (whattext == "Grenade throw")
        {
            whatmessage = 2;
        }
        if (whattext == "Grenade Switch")
        {
            whatmessage = 3;
        }
        if (whattext == "Church Message")
        {
            whatmessage = 4;
        }
        if (whattext == "Melee Switch")
        {
            whatmessage = 5;
        }
        if (whattext == "Bullet Switch")
        {
            whatmessage = 6;
        }
        if (whattext == "Ranged Switch")
        {
            whatmessage = 7;
        }
        #endregion
        texto.text = messages[whatmessage];
    }
    public void HideText()
    {
        texto.text = "";
    }
}
