using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private DoorScript door;
    [SerializeField] GameObject player;
    [SerializeField] private AudioClip keySoundClip;

    CollectableCounter collectableCounter;

    private bool isPickedUp;

    private void Start()
    {
        this.gameObject.SetActive(false);
        collectableCounter = FindObjectOfType<CollectableCounter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            door.UnlockDoor();
            AudioManager.instance.PlaySoundFXClip(keySoundClip, transform, 0.5f);
            Destroy(gameObject);
        }
    }

    public void AllCollected()
    {
        if (collectableCounter.currentCollectable >= 7)
        {
            this.gameObject.SetActive(true);
        }
        else 
        { 
            this.gameObject.SetActive(false); 
        }
    }

}
