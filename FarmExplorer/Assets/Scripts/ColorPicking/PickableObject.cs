using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PickableObject : MonoBehaviour
{
    public enum PickableObjectState
    {
        NONE,
        WRONGPICK,
        CORRECTPICK,
        HIDDEN
    }

    private PickableObjectState state = PickableObjectState.NONE;

    [SerializeField] private float movementSpeed;

    private float shakeSpeed = 30;
    private float shakeDistance = 0.4f;
    private float shakeTime = 0.5f;
    private float shakeTimer = 0;

    private Collider2D thisCollider;
    private Transform thisTransform;

    private ColorPickingManager colorPickingManager;
    private string colorName;
    private Vector3 originalPosition;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
        thisCollider = GetComponent<Collider2D>();
        originalPosition = thisTransform.position;
    }

    void Start()
    {
        colorPickingManager = ColorPickingManager.Instance;
    }

    void Update()
    {
        if (state != PickableObjectState.HIDDEN) Interact();
        HandleState();
        
    }
    public void SetSpriteAndColor(Sprite sprite, Color colorValue, string colorName)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorValue;
        if(sprite != null) spriteRenderer.sprite = sprite;
        this.colorName = colorName;
    }

    public string GetColorName()
    {
        return colorName;
    }

    private void Interact()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

                if (thisCollider == touchedCollider)
                {
                    if (colorName == colorPickingManager.GetColorToPick()) state = PickableObjectState.CORRECTPICK;
                    else state = PickableObjectState.WRONGPICK;
                }
            }
        }
    }

    private void HandleState()
    {
        switch (state)
        {
            case PickableObjectState.NONE:
                return;
            case PickableObjectState.WRONGPICK:
                WrongObjectPicked();
                break;
            case PickableObjectState.CORRECTPICK:
                MoveToStorage();
                break;
            case PickableObjectState.HIDDEN:
                Hide();
                break;
        }
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        thisCollider.enabled = false;
    }

    public void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        thisCollider.enabled = true;
    }

    private void MoveToStorage()
    {
        if (state == PickableObjectState.CORRECTPICK)
        {
            transform.position = Vector3.MoveTowards(transform.position, colorPickingManager.GetPickableObjectStoragePosition(), movementSpeed * Time.deltaTime);
        }
    }

    private void WrongObjectPicked()
    {
        var xOffset = Mathf.Sin(Time.time * shakeSpeed) * shakeDistance;

        thisTransform.position = originalPosition + new Vector3(xOffset, 0, 0) * shakeDistance;
        shakeTimer += Time.deltaTime;
        if (shakeTimer > shakeTime)
        {
            state = PickableObjectState.NONE;
            shakeTimer = 0;
        }
    }

    public void SetPickableObjectStateHidden(bool hide)
    {
        if (hide)
        {
            state = PickableObjectState.HIDDEN;
            Hide();
        }
        else
        {
            state = PickableObjectState.NONE;
            Show();
        }
    }

    public PickableObjectState GetPickableObjectState()
    {
        return state;
    }
}
