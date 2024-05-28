using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotsSound : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (audioSource != null)
            audioSource.Play();
        else
            Debug.LogError("No audio Source");
    }
}
