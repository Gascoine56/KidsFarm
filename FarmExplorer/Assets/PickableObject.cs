using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private Collider2D thisCollider;
    private string colorName;
    private ColorPickingManager colorPickingManager;
    private bool correctPicked = false;
    
    [SerializeField] private float movementSpeed;

    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        colorPickingManager = ColorPickingManager.Instance;
    }

    void Update()
    {
        if (Input.touchCount > 0)Interact();
        if (correctPicked) MoveToStorage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colorPickingManager.CheckIfColorToPickItemsOnScreen();
        Destroy(gameObject);
    }

    private void Interact()
    {
        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
        if (thisCollider == touchedCollider)
        {
            if(colorName == colorPickingManager.GetColorToPick()) correctPicked= true;
            //else shake object
        }
    }

    public void SetSpriteAndColor(Sprite sprite, Color colorValue, string colorName)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorValue;
        spriteRenderer.sprite = sprite;
        this.colorName = colorName;
    }

    private void MoveToStorage()
    {
        transform.position = Vector3.MoveTowards(transform.position, colorPickingManager.GetPickableObjectStoragePosition(), movementSpeed * Time.deltaTime);
        //Play animation or particle effect
    }

    public string GetColorName()
    {
        return colorName;
    }
}
