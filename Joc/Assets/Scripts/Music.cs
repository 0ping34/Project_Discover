using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioClip originalMusicClip;
    public AudioClip creditsMusicClip;

    private bool isCreditsPlaying = false;

    public void PlayCreditsMusic()
    {
        if (!isCreditsPlaying)
        {
            isCreditsPlaying = true;
            backgroundMusic.Stop();
            backgroundMusic.clip = creditsMusicClip;
            backgroundMusic.Play();
        }
    }

    public void SwapToOriginalMusic()
    {
        if (isCreditsPlaying)
        {
            isCreditsPlaying = false;
            backgroundMusic.Stop();
            backgroundMusic.clip = originalMusicClip;
            backgroundMusic.Play();
        }
    }
}