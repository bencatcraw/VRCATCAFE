using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private int i=0;
    private bool isPlaying = true;

    private void Start()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void nextSong()
    {
        i++;
        if(i == audioClips.Length)
        {
            i = 0;
        }
        audioSource.clip = audioClips[i];
        audioSource.Play();
    }
    public void playPauseAudio()
    {
        if (isPlaying)
        {
            audioSource.Pause();
            isPlaying = false;
        }
        else
        {
            audioSource.Play();
            isPlaying = true;
        }
    }
}
