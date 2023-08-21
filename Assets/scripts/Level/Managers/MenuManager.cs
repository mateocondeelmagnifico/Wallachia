using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObjectgetter getter;
    GameObject currentmenu;

    
    GameObject[] menus;

    int menunumber;
    void Start()
    {
        //Here are the different menus that appear
        menus = new GameObject[4];
        menus[0] = getter.gamepausedmenu;
        menus[1] = getter.controlsmenu;
        menus[2] = getter.gameovermenu;
        menus[3] = getter.WeaponWheelMenu;
    }
    public void changemenu(string whichMenu)
    {
        //This disables the currentmenu and opens the next one

        if (currentmenu!= null) 
        {
            currentmenu.SetActive(false);
        }

        if (whichMenu == "pause")
        {
            menunumber = 0;
        }
        if (whichMenu == "controls")
        {
            menunumber = 1;
        }
        if (whichMenu == "game over")
        {
            menunumber = 2;
        }
        if (whichMenu == "weapon wheel") 
        {
            menunumber = 3;
        }

        //This is so that whatever menu you're on is disabled when you go to the main screen
        if (whichMenu != "none")
        {
            menus[menunumber].SetActive(true);
        }
        
        currentmenu = menus[menunumber];

    }
}
