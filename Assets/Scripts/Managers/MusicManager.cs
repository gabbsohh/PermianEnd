using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip bgMusic;
    [SerializeField] private AudioSource musicObject;
    public static AudioManager instance;
    
    private void Start()
    {
        AudioManager.instance.PlaySoundFXClip(bgMusic, transform, 0.3f);   
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn Game Object
        AudioSource audioSource = Instantiate(musicObject, spawnTransform.position, Quaternion.identity);

        // Assign Audio Clip
        audioSource.clip = audioClip;

        // Assign Volume
        audioSource.volume = volume;

        // Play Sound
        audioSource.Play();

        // Get Length of Music Clip Clip
        float clipLength = audioSource.clip.length;

        // Destroy Clip After It's Finished Playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
