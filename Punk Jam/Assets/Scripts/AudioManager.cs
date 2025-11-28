using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void PlayAudioOneShot(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip,volume);
    } 
}
