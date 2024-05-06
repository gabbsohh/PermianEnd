using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private DoorScript door;
    [SerializeField] GameObject player;

    CollectableCounter collectableCounter;

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
            Debug.Log("Key picked up");
            door.UnlockDoor();
            Destroy(gameObject);
        }
    }


}
