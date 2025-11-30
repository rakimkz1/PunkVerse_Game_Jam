using System;
using System.Threading.Tasks;
using UnityEngine;

public class SoundTrackControal : MonoBehaviour
{
    public int ofset;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        OffsetWait();
    }

    private async Task OffsetWait()
    {
        await Task.Delay(ofset);
        audioSource.Play();
    }
}
