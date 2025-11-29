using UnityEngine;

public class SoundTrackManager : MonoBehaviour
{
    public static SoundTrackManager Instance;
    private AudioSource soundTrack;

    private void Start()
    {
        Instance = this;
    }

    public void SetVolume(float volume)
    {
        soundTrack.volume = volume;
    }
}
