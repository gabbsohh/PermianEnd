using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private AudioClip healthSoundClip;
    public int healAmount = 1; // Amount of health to increase when collected

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<PlayerHealth>().CollectHealthCollectable(healAmount);
            AudioManager.instance.PlaySoundFXClip(healthSoundClip, transform, 0.5f);
        }
    }
}
