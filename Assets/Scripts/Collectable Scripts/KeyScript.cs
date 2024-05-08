using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] GameObject player;
    [SerializeField] private AudioClip keySoundClip;

    private bool isPickedUp;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public TextMeshProUGUI doorUnlockText;

    private void Start()
    {
        doorUnlockText.text = " ";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            door.GetComponent<DoorScript>().UnlockDoor();
            AudioManager.instance.PlaySoundFXClip(keySoundClip, transform, 0.5f);       
            doorUnlockText.text = "Door Unlocked!";
            Destroy(gameObject);
        }
    }
}
