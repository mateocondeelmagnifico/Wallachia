using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheel : MonoBehaviour
{
    public GameObject[] icons;
    public weapons armas;
    public Sprite[] grenadeSprites;
    public Sprite[] bulletSprites;
    public Text[] descriptions;
    void Start()
    {
        icons[2].SetActive(false);
        icons[3].SetActive(false);
        icons[4].SetActive(false);
        icons[5].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        #region Check what you have unlocked
        if (armas.hasaxe)
        {
            icons[2].SetActive(true);
        }
        if (armas.hasrifle)
        {
            icons[3].SetActive(true);
        }
        if (armas.hasgrenade)
        {
            icons[4].SetActive(true);
            descriptions[0].enabled = true;
        }
        if (armas.hasbullet)
        {
            icons[5].SetActive(true);
            descriptions[1].enabled = true;
        }
        #endregion

        #region Bullet and grenade changes
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

        if (armas.tempEquip[3] == 0)
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
