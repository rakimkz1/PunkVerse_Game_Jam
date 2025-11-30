using System;
using System.Threading.Tasks;
using UnityEngine;

public class SoundTrackManager : MonoBehaviour
{
    public static SoundTrackManager Instance;
    public int offset;
    private AudioSource soundTrack;

    private void Start()
    {
        Instance = this;
        soundTrack = GetComponent<AudioSource>();
        PlaySoundTrack();
    }

    private async Task PlaySoundTrack()
    {
        soundTrack.Stop();
        await Task.Delay(offset);
        soundTrack.Play();
        await Task.Delay(276000);
        PlaySoundTrack();
    }

    public void SetVolume(float volume)
    {
        soundTrack.volume = volume;
    }
}
