using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] GameObject backGroundImage;
    [SerializeField] GameObject icon;
    [SerializeField] private AudioClip enterCollisionSound;
    [SerializeField] private Vector3 activeScaleBackGround;
    [SerializeField] private Vector3 activeScaleIcon;
    [SerializeField] private SceneAsset sceneToEnter;

    private AudioSource audioSource;
    private Collider2D thisCollider;

    private Vector3 originalScaleBackGround;
    private Vector3 originalScaleIcon;
    private float originalAlpha;
    private float activeAlpha = 1;
    private bool isActive = false;

    private Transform player;


    private void Awake()
    {
        thisCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = enterCollisionSound;

        originalScaleBackGround = backGroundImage.transform.localScale;
        originalScaleIcon = icon.transform.localScale;
        originalAlpha = backGroundImage.GetComponent<SpriteRenderer>().color.a;
    }

    private void Update()
    {
        if (isActive)
        {
            HandleInteraction();
        }
    }

    private void Start()
    {
        player = Vehicle.Instance.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.transform == player)
        {
            SetImagesVisualActive();
            isActive = true;
            audioSource.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SetImagesVisualInActive();
        isActive = false;
    }

    private void SetImagesVisualActive()
    {
        backGroundImage.transform.localScale = activeScaleBackGround;
        icon.transform.localScale = activeScaleIcon;
        backGroundImage.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, activeAlpha - originalAlpha);
        icon.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, activeAlpha - originalAlpha);
    }

    private void SetImagesVisualInActive()
    {
        backGroundImage.transform.localScale = originalScaleBackGround;
        icon.transform.localScale = originalScaleIcon;
        backGroundImage.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, activeAlpha - originalAlpha);
        icon.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, activeAlpha - originalAlpha);
    }

    private void HandleInteraction()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
            Collider2D vehicleCollider = player.GetComponent<Collider2D>();

            if (thisCollider == touchedCollider || touchedCollider == vehicleCollider)
            {
                SceneTransferManager.Instance.LoadLevel(sceneToEnter.name, Vehicle.Instance.transform.position, Vehicle.Instance.GetComponentInChildren<SpriteRenderer>().sprite);
            }
        }
    }
}
