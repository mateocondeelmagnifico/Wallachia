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
        }
        if (armas.hasbullet)
        {
            icons[5].SetActive(true);
        }
        #endregion

        #region Bullet and grenade changes
        if (armas.tempEquip[2] == 0)
        {
            icons[4].GetComponent<Image>().sprite = grenadeSprites[0];
        }
        else
        {
            icons[4].GetComponent<Image>().sprite = grenadeSprites[1];
        }

        if (armas.tempEquip[3] == 0)
        {
            icons[5].GetComponent<Image>().sprite = bulletSprites[0];
        }
        else
        {
            icons[5].GetComponent<Image>().sprite = bulletSprites[1];
        }
        #endregion
    }
}
