using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private Collider2D thisCollider;

    void Start()
    {
        thisCollider= GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Interact();
        }
    }

    private void Interact()
    {
        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

        if (thisCollider == touchedCollider)
        {
            
        }
    }
}
