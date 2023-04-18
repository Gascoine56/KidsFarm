using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenManager : MonoBehaviour
{
    public static HenManager Instance { get; private set; }
    private SoundManager soundManager;

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private List<Sprite> henSprites;

    private void Awake()
    {
        if (Instance != null) print("There is more than one Player instance");
        Instance = this;

        AssignSpritesToHen();
    }
    private void Start()
    {
        soundManager = SoundManager.Instance;
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
        soundManager.PlaySoundClip(clipToPlay);
    }
}
