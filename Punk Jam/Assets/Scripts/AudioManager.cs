using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public static AudioManager instance;
    private float sfx_volume;
    private void Awake()
    {
        instance = this;
        Settings.instance.OnSFXVolumeChanged += SetVolume;
    }
    
    public void SetVolume(float volume)
    {
        sfx_volume = volume;
    }

    public void PlayAudioOneShot(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip,volume * sfx_volume);
    }
}
