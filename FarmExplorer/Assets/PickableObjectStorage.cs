using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectStorage : MonoBehaviour
{
    private const string CHANGE_COLOR_ANIMATION_TRIGGER = "ColorChange";

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] AudioClip correctPickClip;

    private SpriteRenderer spriteRenderer;
    private ColorPickingManager colorPickingManager;
    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        colorPickingManager = ColorPickingManager.Instance;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
        ChangeBoxColorAnimation();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PickableObject>())
        {
            PickableObject pickableObject = collision.GetComponent<PickableObject>();
            PlaySuccessParticles();
            PlaySuccessSound();
            collision.gameObject.SetActive(false);
            colorPickingManager.CheckIfColorToPickItemsOnScreen();
        }
    }

    private void PlaySuccessParticles()
    {
        if (successParticles.isPlaying) successParticles.Stop();
        var particleMain = successParticles.main;
        particleMain.startColor = spriteRenderer.color;
        successParticles.Play();
    }
    public void ChangeBoxColorAnimation()
    {
        animator.SetTrigger(CHANGE_COLOR_ANIMATION_TRIGGER);
    }
    private void PlaySuccessSound()
    {
        audioSource.clip = correctPickClip;
        audioSource.Play();
    }
}
