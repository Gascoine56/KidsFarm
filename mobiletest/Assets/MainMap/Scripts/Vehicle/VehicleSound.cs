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
        if(vehicle.GetIsMoving() && audioSource.clip != drivingClip)
        {
            audioSource.clip = drivingClip;
            audioSource.Play();
        }
        else if(!vehicle.GetIsMoving() && audioSource.clip != idleClip)
        {
            audioSource.clip = idleClip;
            audioSource.Play();
        }
    }
}
