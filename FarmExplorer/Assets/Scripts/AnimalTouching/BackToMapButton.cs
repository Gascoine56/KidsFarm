using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackToMapButton : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private Collider2D boxCollider;
    private SoundManager soundManager;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

            if (boxCollider == touchedCollider)
            {                
                soundManager.PlaySoundClip(audioClip);
                BackToMap();
            }
        }
    }
    public void BackToMap()
    {
        SceneTransferManager.Instance.ReturnToMapScene();
    }


}
