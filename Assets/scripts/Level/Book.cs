using UnityEngine;
using UnityEngine.UI;
using WeaponMechanics;

public class Book : LevelInteractable
{
    //Mini tutorials and lore entries for the players to read
    [Header("Put here the thing this book unlocks, use uppercase")]
    public string whatPowerUp;

    public GameObject pausemanager;
    public GameObject noteobject;
    public GameObject key;
    public GameObject pickUp;

    public Sprite mypage;
    Image image;

    Pause pausa;
    weapons armas;

    bool isreading;

    // Start is called before the first frame update
    void Start()
    {
        image = noteobject.GetComponent<Image>();
        pausa = pausemanager.GetComponent<Pause>();
        RespawnManager resManager = RespawnManager.instance;
        if (resManager.unlockedThings.Contains(whatPowerUp)) Deactivate(pickUp);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isreading == true)
        {
            armas.playsound();
            armas.UnlockWeapon(whatPowerUp);
            stopreading();
            Destroy(pickUp);
        }

        KeyCheck();
    }
    private void read(Sprite whatpage)
    {
        image.sprite = whatpage;
        pausa.isonmenu = true;
        pausa.pause("partial");
        image.enabled = true;
        isreading = true;
    }
    private void stopreading()
    {
        image.enabled = false;
        pausa.unpause();
        isreading = false;
    }
    
    public override void Interact(GameObject player)
    {

        armas = player.GetComponent<weapons>();

        if (isreading == false)
        {
            read(mypage);
        }
        else
        {
            armas.playsound();
            armas.UnlockWeapon(whatPowerUp);
            stopreading();
            Deactivate(pickUp);
        }
    }
}
