using UnityEngine;
using UnityEngine.UI;
using WeaponMechanics;

public class Book : MonoBehaviour
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
    RespawnManager respawnmanager;
    weapons armas;

    public bool isnear;
    bool isreading;
    bool weaponUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        image = noteobject.GetComponent<Image>();
        pausa = pausemanager.GetComponent<Pause>(); 
        respawnmanager = FindObjectOfType<RespawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isnear == true)
        {
            if (isreading == false)
            {
                read(mypage);
            }
            else
            {
                armas.playsound();
                armas.UnlockWeapon(whatPowerUp);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isreading == true)
        {
            armas.playsound();
            armas.UnlockWeapon(whatPowerUp);
            stopreading();
        }

        CheckUnlock(whatPowerUp);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            armas = other.GetComponent<weapons>();
            key.SetActive(true);
            isnear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isnear = false;
            key.SetActive(false);
        }
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
    public void CheckUnlock(string whatUnlock)
    {
        if (whatUnlock == "Rifle")
        {
            if (!respawnmanager.hasRifle)
            {
                weaponUnlocked = false;
            }
            else
            {
                pickUp.SetActive(false);
            }
        }
        if (whatUnlock == "Axe")
        {
            if (!respawnmanager.hasAxe)
            {
                weaponUnlocked = false;
            }
            else
            {
                pickUp.SetActive(false);
            }
        }
        if (whatUnlock == "Bullet")
        {
            if (!respawnmanager.hasBullets)
            
                {
                    weaponUnlocked = false;
                }
            else
                {
                    pickUp.SetActive(false);
                }
            }
        if (whatUnlock == "Grenade")
        {
            if (!respawnmanager.hasGrenades)
            {
                weaponUnlocked = false;
            }
            else
            {
                pickUp.SetActive(false);
            }
        }
        if (whatUnlock == "Sword")
        {
            if (!respawnmanager.hasSword)
            {
                weaponUnlocked = false;
            }
            else
            {
                pickUp.SetActive(false);
            }
        }
    }
}
