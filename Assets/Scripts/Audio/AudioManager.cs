using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource ambientSound;
    public AudioSource victoryMusic;
    public AudioSource defeatSound;
    public AudioSource coinCollectedSFX;
    public AudioSource powerUpCollectedSFX;

    // private bool isVictoryMusicPlaying = false;

    void Start()
    {
        // Check If the setup are correctly
        if (ambientSound == null || victoryMusic == null || defeatSound == null)
        {
            Debug.LogError("Certifique-se de atribuir as fontes de áudio no Inspector.");
            return;
        }

        // Start ambience sound in loop
        PlayAmbientSound();
    }

    public void PlayAmbientSound()
    {
        if (ambientSound != null)
        {
            ambientSound.Play();
            ambientSound.loop = true;
        }
    }

    public void PlayVictoryMusic()
    {
        if (victoryMusic != null)
        {
            //Stop ambience sound
            if (ambientSound.isPlaying)
            {
                ambientSound.Pause();
            }

            victoryMusic.Play();
            // isVictoryMusicPlaying = true;
        }
    }

    public void PlayDefeatSound()
    {
        if (defeatSound != null)

        {
            //Stop ambience sound
            if (ambientSound.isPlaying)
            {
                ambientSound.Pause();
            }
        }
        defeatSound.Play();
    }

    public void CoinCollectedSFX()
    {
        if (coinCollectedSFX != null)
        {
            coinCollectedSFX.Play();
        }
    }

    public void PowerUpCollectedSFX()
    {
        if (powerUpCollectedSFX != null)
        {
            powerUpCollectedSFX.Play();
        }
    }


}
