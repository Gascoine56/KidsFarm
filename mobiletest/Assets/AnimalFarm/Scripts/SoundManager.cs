using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;

    private bool canPlaySoundClip = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying) SetCanPlaySoundClip(true);
    }

    public void PlaySoundClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        SetCanPlaySoundClip(false);
    }

    public bool GetCanPlaySoundClip()
    {
        return canPlaySoundClip;
    }

    public void SetCanPlaySoundClip(bool isPlayingSoundClip)
    {
        this.canPlaySoundClip = isPlayingSoundClip;
    }
}
