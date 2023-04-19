using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hen : MonoBehaviour
{
    [SerializeField] private Transform moveToPoint;
    [SerializeField] List<PickableObject> eggsToHide;

    private HenManager henManager;
    private Collider2D circleCollider;

    private bool hasMoved = false;

    private void Awake()
    {
        circleCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        henManager = HenManager.Instance;
        HideEggs();
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
            if (circleCollider == touchedCollider)
            {
                henManager.PlaySound();
                if (!hasMoved)
                {
                    MoveToPosition();
                    ShowEggs();
                }
            }
        }
    }

    private void MoveToPosition()
    {
        transform.position = moveToPoint.position;
        hasMoved = true;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }

    private void HideEggs()
    {
        foreach (var egg in eggsToHide)
        {
            egg.Hide();
        }
    }

    private void ShowEggs()
    {
        foreach (var egg in eggsToHide)
        {
            egg.Show();
        }
    }
}
