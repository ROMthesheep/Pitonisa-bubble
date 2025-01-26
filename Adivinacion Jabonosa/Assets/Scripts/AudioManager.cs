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

    public void PlayMenuMusic()
    {
        StopAllClips();
        menuSource.PlayOneShot(menuSource.clip);
    }

    public void PlayGameplayMusic()
    {
        StopAllClips();
        gameplaySource.PlayOneShot(gameplaySource.clip);
    }

    public void PlayDialogueMusic()
    {
        StopAllClips();
        dialogueSource.PlayOneShot(dialogueSource.clip);
    }

    public void PlayTensionMusic()
    {
        StopAllClips();
        tensionSource.PlayOneShot(tensionSource.clip);
    }
    public void PlayEndMusic()
    {
        StopAllClips();
        endSource.PlayOneShot(endSource.clip);
    }

    public void StopAllClips()
    {
        menuSource.Stop();
        gameplaySource.Stop();
        dialogueSource.Stop();
        tensionSource.Stop();
        endSource.Stop();
    }
}
