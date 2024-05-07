using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn Game Object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign Audio Clip
        audioSource.clip = audioClip;

        // Assign Volume
        audioSource.volume = volume;

        // Play Sound
        audioSource.Play();

        // Get Length of Sound FX Clip
        float clipLength = audioSource.clip.length;

        // Destroy Clip After It's Finished Playing
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        // Add Random Index
        int rand = Random.Range(0, audioClip.Length);

        // Spawn Game Object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign Audio Clip
        audioSource.clip = audioClip[rand];

        // Assign Volume
        audioSource.volume = volume;

        // Play Sound
        audioSource.Play();

        // Get Length of Sound FX Clip
        float clipLength = audioSource.clip.length;

        // Destroy Clip After It's Finished Playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
