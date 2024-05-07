using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] GameObject player;
    //[SerializeField] private AudioClip keySoundClip;

    private bool isPickedUp;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            door.GetComponent<DoorScript>().UnlockDoor();
            //AudioManager.instance.PlaySoundFXClip(keySoundClip, transform, 0.5f);
            Destroy(gameObject);
        }
    }

}
