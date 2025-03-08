using PlayerMechanics;
using UnityEngine;

public class Door : LevelInteractable
{
    [SerializeField] private int nextLocation;
    [SerializeField] private Transform nextSceneObjects, prevSceneObjects;
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

        if (!isUsed)
        {
            soundManager.ChangeArea(nextLocation);
            LoadObjects(nextSceneObjects, prevSceneObjects);
            isUsed = true;
        }
        else
        {
            soundManager.ChangeArea(nextLocation - 1);
            LoadObjects(prevSceneObjects, nextSceneObjects);
            isUsed = false;
        }
    }

    public void LoadObjects(Transform objectToLoad, Transform objectToHide)
    {

        for(int i = 0; i < objectToLoad.childCount; i++)
        {
            objectToLoad.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < objectToHide.childCount; i++)
        {
            objectToHide.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
