using UnityEngine;

public class Scaryness : MonoBehaviour
{
    //Esto controla el miedo que le tienen los enemigos al jugador
    public static Scaryness Instance { get; private set; }

    private float howScary, timer, secondaryTimer;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        //Mantines la furia mientras que estes en combate basicamente
        if(secondaryTimer > 0)
        {
            secondaryTimer-= Time.deltaTime;   
        }
        else
        {
            timer -=Time.deltaTime;
        }
    }

    public void IncreaseScaryness(float howMuch)
    {
        howScary += howMuch;

        secondaryTimer = 3;

        timer += howMuch * 2;

        if (timer > 15) timer = 15;
    }
}
