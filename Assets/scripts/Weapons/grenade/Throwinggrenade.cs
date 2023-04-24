using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throwinggrenade : MonoBehaviour
{
    public GameObject player;
    public GameObject grenade;
    public GameObject text;
    public GameObject soundmanager;
    GameObject currentgrenade;

    public float grenadecooldown;

    public int remaininggarlic;
    public int remainingsilver;

    public Text texto;

    Sonido sound;

    public bool grenadesleft;
    public bool ispaused;

    weapons armas;

    // Start is called before the first frame update
    void Start()
    {
        armas = player.GetComponent<weapons>();
        texto = text.GetComponent<Text>();
        remaininggarlic = 2;
        remainingsilver = 2;
        sound = soundmanager.GetComponent<Sonido>();
    }

    // Update is called once per frame
    void Update()
    {
        if (armas.currentgrenade == 0 && remainingsilver > 0 || armas.currentgrenade == 1 && remaininggarlic > 0)
        {
            grenadesleft = true;
        }
        else
        {
            grenadesleft = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && grenadecooldown <= 0 && grenadesleft == true && ispaused == false)
        {
            currentgrenade = GameObject.Instantiate(grenade, transform.position, transform.rotation);
            currentgrenade.GetComponent<grenade>().armas = armas;
            currentgrenade.GetComponent<grenade>().sound = sound;
            grenadecooldown = 1;
            if (armas.currentgrenade == 0)
            {
                remainingsilver--;
            }
            else
            {
                remaininggarlic--;
            }
        }
        if (grenadecooldown > 0)
        {
            grenadecooldown -= Time.deltaTime;
        }

        if (armas.currentgrenade == 0)
        {
            texto.text = remainingsilver.ToString();
        }
        else
        {
            texto.text = remaininggarlic.ToString();
        }
    }
}
