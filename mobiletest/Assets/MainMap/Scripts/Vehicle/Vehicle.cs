using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public static Vehicle Instance { get; private set; }

    [SerializeField] float moveSpeed;
    [SerializeField] SpriteRenderer spriteRenderer;

    private Vector2 target = Vector2.zero;
    private bool isMoving = false;

    private void Awake()
    {
        if (Instance != null) print("There is more than one Player instance");
        Instance = this;
    }

    void Update()
    {
        HandleMovement();
        ChangeVisual();
    }

    private void HandleMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            target = Camera.main.ScreenToWorldPoint(touch.position);
        }
        Vector2 moveVector = Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        transform.position = moveVector;

        isMoving = moveVector != target;
        Debug.Log(moveVector);
    }

    private void ChangeVisual()
    {
        if (target.x > transform.position.x) spriteRenderer.flipX = true;
        else if (target.x < transform.position.x) spriteRenderer.flipX = false;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }
}
