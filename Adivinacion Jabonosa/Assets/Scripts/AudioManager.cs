using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource menuSource;
    [SerializeField] AudioSource gameplaySource;
    [SerializeField] AudioSource dialogueSource;
    [SerializeField] AudioSource tensionSource;
    [SerializeField] AudioSource endSource;
    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioClip[] bubblePop;

    public void PlayMenuMusic()
    {
        StopAllClips();
        menuSource.Play();
    }

    public void PlayGameplayMusic()
    {
        StopAllClips();
        gameplaySource.Play();
    }

    public void PlayDialogueMusic()
    {
        StopAllClips();
        dialogueSource.Play();
    }

    public void PlayTensionMusic()
    {
        StopAllClips();
        tensionSource.Play();
    }
    public void PlayEndMusic()
    {
        StopAllClips();
        endSource.Play();
    }

    public void PlayBubblePop()
    {
        StopAllClips();
        soundEffects.PlayOneShot(bubblePop[Random.Range(0, bubblePop.Length)]);
    }

    public void StopAllClips()
    {
        menuSource.Stop();
        gameplaySource.Stop();
        dialogueSource.Stop();
        tensionSource.Stop();
        endSource.Stop();
        soundEffects.Stop();
    }
}
