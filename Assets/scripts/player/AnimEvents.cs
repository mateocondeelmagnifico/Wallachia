
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    // This script is only to call soundmanager for the player footsteps
    public Sonido sound;
    public void callSoundManager(string whatSound)
    {
        sound.playaudio(whatSound);
    }
}
