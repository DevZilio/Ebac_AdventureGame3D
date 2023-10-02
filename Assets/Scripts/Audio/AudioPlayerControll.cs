using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerControll : MonoBehaviour
{
    public AudioSource stepSound;
    // public AudioSource bangSound;

    public void WalkSFX()
    {
        if (stepSound != null) stepSound.Play();
    }

    
}  