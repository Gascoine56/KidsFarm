using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishedButtonController : MonoBehaviour
{
    private ColorPickingManager colorPickingManager;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        colorPickingManager = ColorPickingManager.Instance;
    }
    public void ReplayButton()
    {
        PlayClickSound();
        colorPickingManager.SetReplayCanvasActiveState(false);
        colorPickingManager.RestartScene();
    }
    public void BackToMap()
    {
        PlayClickSound();
        SceneTransferManager.Instance.ReturnToMapScene();
    }

    public void PlayClickSound()
    {
        audioSource.Play();
    }
}
