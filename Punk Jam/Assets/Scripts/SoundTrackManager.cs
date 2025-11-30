using System;
using System.Collections;
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
        StartCoroutine(PlaySoundTracck());
    }


    IEnumerator PlaySoundTracck()
    {
        soundTrack.Stop();
        yield return new WaitForSeconds((float)offset / 1000);
        soundTrack.Play();
        yield return new WaitForSeconds(276f);
        StartCoroutine(PlaySoundTracck());
    }

    public void SetVolume(float volume)
    {
        soundTrack.volume = volume;
    }
}
