using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectStorage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ColorPickingManager colorPickingManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        colorPickingManager = ColorPickingManager.Instance;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        colorPickingManager.CheckIfColorToPickItemsOnScreen();
    }
}
