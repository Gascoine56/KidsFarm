using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenManager : MonoBehaviour
{
    public static HenManager Instance { get; private set; }

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private List<Sprite> henSprites;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null) print("There is more than one Player instance");
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        AssignSpritesToHen();
    }

    private void AssignSpritesToHen()
    {
        Hen[] henObjects = (Hen[])FindObjectsOfType(typeof(Hen));
        foreach (Hen hen in henObjects)
        {
            Sprite spriteToAssign = henSprites[Random.Range(0, henSprites.Count)];

            hen.GetSpriteRenderer().sprite = spriteToAssign;
            henSprites.Remove(spriteToAssign);
        }
    }

    public void PlaySound()
    {
        AudioClip clipToPlay = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
}
