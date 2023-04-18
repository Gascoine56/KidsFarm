using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSound : MonoBehaviour
{
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip drivingClip;
    [SerializeField] private AudioSource audioSource;

    private Vehicle vehicle;

    private void Start()
    {
        vehicle = Vehicle.Instance;
    }
    private void Update()
    {
        if (SceneTransferDataStore.GetGameStarted())
        {
            if (vehicle.GetIsMoving() && audioSource.clip != drivingClip)
            {
                PlayClip(drivingClip);
            }
            else if (!vehicle.GetIsMoving() && audioSource.clip != idleClip)
            {
                PlayClip(idleClip);                
            }
        }
    }

    private void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
