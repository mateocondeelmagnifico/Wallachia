using UnityEngine;
using UnityEngine.UI;
using WeaponMechanics;

public class WeaponWheel : MonoBehaviour
{
    public GameObject[] icons;
    public weapons armas;
    public Sprite[] grenadeSprites;
    public Sprite[] bulletSprites;
    public Text[] descriptions;
    void Start()
    {
        //Desactivar iconos

        for(int i = 0; i < 6; i++)
        {
            if(i != 1)
            icons[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Check what you have unlocked
        if (armas.hassword) icons[0].SetActive(true);
        if (armas.hasaxe)
        {
            icons[2].SetActive(true);
        }
        if (armas.hasrifle)
        {
            icons[3].SetActive(true);
        }
        #endregion

        #region Bullet and grenade changes
        /*
        if (armas.tempEquip[2] == 0)
        {
            icons[4].GetComponent<Image>().sprite = grenadeSprites[0];
            descriptions[0].text = "Silver Grenades";
        }
        else
        {
            icons[4].GetComponent<Image>().sprite = grenadeSprites[1];
            descriptions[0].text = "Garlic Grenades";
        }
        */

        if (armas.currentBullet == weapons.bulletTypes.iron)
        {
            icons[5].GetComponent<Image>().sprite = bulletSprites[0];
            descriptions[1].text = "Normal Bullets";
        }
        else
        {
            icons[5].GetComponent<Image>().sprite = bulletSprites[1];
            descriptions[1].text = "Silver Bullets";
        }
        #endregion
    }
}
