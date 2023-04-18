using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hen : MonoBehaviour
{
    [SerializeField] private Transform moveToPoint;

    private HenManager henManager;
    private Vector3 originalPosition;
    private Collider2D circleCollider;

    private bool isInOriginalPosition = true;
    List<GameObject> currentCollisions = new List<GameObject>();

    private void Awake()
    {
        originalPosition = transform.position;
        circleCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        henManager = HenManager.Instance;
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
                SetPosition();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickableObject po = collision.gameObject.GetComponent<PickableObject>();
        po.SetPickableObjectStateHidden(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PickableObject po = collision.gameObject.GetComponent<PickableObject>();
        po.SetPickableObjectStateHidden(false);
    }

    private void SetPosition()
    {
        if (isInOriginalPosition)
        {
            transform.position = moveToPoint.position;
            isInOriginalPosition = false;
        }
        else
        {
            transform.position = originalPosition;
            isInOriginalPosition = true;
        }
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }
}
