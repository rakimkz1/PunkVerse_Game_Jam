using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class SoundTrackManager : MonoBehaviour
{
    public static SoundTrackManager Instance;
    public int offset;
    private AudioSource soundTrack;
    private float audioAmount;

    private void Start()
    {
        Instance = this;
        soundTrack = GetComponent<AudioSource>();
        soundTrack.volume = audioAmount;
        Settings.instance.OnMusicVolumeChanged += SetMusicVolume;
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

    public void SetMusicVolume(float volume)
    {
        audioAmount = volume;
    }
}
