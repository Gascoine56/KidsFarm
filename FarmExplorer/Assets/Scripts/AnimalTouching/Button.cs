using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    private Collider2D boxCollider;
    private SoundManager soundManager;
    private BUttonVisualScript visual;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;
        visual = GetComponentInChildren<BUttonVisualScript>();
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (soundManager.GetCanPlaySoundClip())
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

                if (boxCollider == touchedCollider)
                {
                    PlaySound();
                    if (visual != null) visual.SetSelectedVisual();
                }
            }
        }
    }

    private void PlaySound()
    {
        AudioClip clipToPlay = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        soundManager.PlaySoundClip(clipToPlay);
    }
}
