using PlayerMechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : LevelInteractable
{
    [SerializeField] private int nextLocation;
    private Sonido soundManager;
    private bool isUsed;

    private void Start()
    {
        soundManager = Sonido.instance;
    }

    public override void Interact(GameObject player) 
    {
        //Play animation, change music and reduce rage

        player.GetComponent<Scaryness>().timer = 0;
        keyPrompt.SetActive(false);

        if (nextLocation == 0) return;
        if(!isUsed)
        {
            soundManager.ChangeArea(nextLocation);
            isUsed = true;
        }
        else
        {
            soundManager.ChangeArea(nextLocation - 1);
            isUsed = false;
        }
    }
}
