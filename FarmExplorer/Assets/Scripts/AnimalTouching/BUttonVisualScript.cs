using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUttonVisualScript : MonoBehaviour
{
    SoundManager soundManager;

    private SpriteRenderer spriteRenderer;
    private Color spriteColorOriginal;
    private Color spriteColorSelected = Color.white;
    private Vector3 scaleOriginal;
    private Vector3 scaleSelected;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteColorOriginal = spriteRenderer.color;
        scaleOriginal= transform.localScale;
        scaleSelected = scaleOriginal * 1.5f;
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;
    }

    private void Update()
    {
        if (soundManager.GetCanPlaySoundClip())
        {
            transform.localScale = scaleOriginal;
            spriteRenderer.color = spriteColorOriginal;
        }
    }

    public void SetSelectedVisual()
    {
        transform.localScale = scaleSelected;
        spriteRenderer.color = spriteColorSelected;
    }
}
