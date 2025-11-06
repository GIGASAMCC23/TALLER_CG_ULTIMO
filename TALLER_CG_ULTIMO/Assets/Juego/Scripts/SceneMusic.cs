using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f; 
        audioSource.Play();
    }
}
