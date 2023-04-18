using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public static Vehicle Instance { get; private set; }

    [SerializeField] float moveSpeed;
    [SerializeField] SpriteRenderer spriteRenderer;

    private Collider2D vehicleCollider;
    private bool isMoving;

    private void Awake()
    {
        if (Instance != null) print("There is more than one Player instance");
        Instance = this;
        vehicleCollider = GetComponent<Collider2D>();
        SetVehicleData();
    }

    private void SetVehicleData()
    {
        transform.position = SceneTransferDataStore.GetVehicleLocation();
        spriteRenderer.sprite = SceneTransferDataStore.GetSelectedVehicleSprite();
    }

    void Update()
    {
        if (SceneTransferDataStore.GetGameStarted())
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 target = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 moveVector = Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
            transform.position = moveVector;
            ChangeVisual(target.x);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void ChangeVisual(float targetX)
    {
        if (targetX > transform.position.x) spriteRenderer.flipX = true;
        else if (targetX < transform.position.x) spriteRenderer.flipX = false;
    }

    public void ChangeVehicleSprite()
    {
        spriteRenderer.sprite = SceneTransferDataStore.GetSelectedVehicleSprite();
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }
}
