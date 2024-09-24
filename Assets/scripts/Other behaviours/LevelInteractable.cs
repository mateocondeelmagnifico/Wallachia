using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))] 

public class LevelInteractable : MonoBehaviour
{
    //Script para objetos con los que el player interactue
    public GameObject keyPrompt;
    private GameObject player;
    private bool canInteract;

    private void Update()
    {
        KeyCheck();
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (player == null) player = other.gameObject;
            keyPrompt.SetActive(true);
            canInteract = true;
        } 
    }
    public virtual void Interact(GameObject player) { }

    private void OnTriggerExit(Collider other)
    {
        //Hide key prompt
        if (other.CompareTag("Player"))
        {
            keyPrompt.SetActive(false);
            canInteract = false;
        }
    }

    public void Deactivate(GameObject prop)
    {
        //after they are used some of these destroy themselves
        Destroy(keyPrompt);
        GetComponent<Collider>().enabled = false;
        if (prop != null) Destroy(prop);
        Destroy(this);
    }

    public void KeyCheck()
    {
        //Esto se mete en el update de los hijos

        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Interact(player);
        }
    }
}
