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

    private Vector2 target;
    private bool isMoving = false;

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
        target = transform.position;
        spriteRenderer.sprite = SceneTransferDataStore.GetSelectedVehicleSprite();
    }

    void Update()
    {
        if (SceneTransferDataStore.GetGameStarted())
        {
            HandleMovement();
            ChangeVisual();
        }
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
    }

    private void ChangeVisual()
    {
        if (target.x > transform.position.x) spriteRenderer.flipX = true;
        else if (target.x < transform.position.x) spriteRenderer.flipX = false;
    }

    public void ChangeVehicleSprite()
    {
        spriteRenderer.sprite = SceneTransferDataStore.GetSelectedVehicleSprite();
        Vehicle.Instance.ResetTarget();
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }  

    public void ResetTarget()
    {
        target = Vector2.zero;
    }
}
