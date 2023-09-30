
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    // This script is only to call soundmanager for the player footsteps
    public Sonido sound;
    int leftCounter = 1;
    int rightCounter = 1;

    public void callSoundManager(string whatSound)
    {
        sound.playaudio(whatSound);
    }

    public void callFootSteps(string whatFootstep)
    {
        //This code calls different footstep sounds so that it doesnt sound monotone
        if(whatFootstep == "Left")
        {
            callSoundManager("Footstep L" + leftCounter.ToString());
            if (leftCounter < 2)
            {
                leftCounter++;
            }
            else
            {
                leftCounter = 1;
            }
        }
        else
        {
            callSoundManager("Footstep R" + leftCounter.ToString());
            if (rightCounter < 2)
            {
                rightCounter++;
            }
            else
            {
                rightCounter = 1;
            }
        }
    }
}
