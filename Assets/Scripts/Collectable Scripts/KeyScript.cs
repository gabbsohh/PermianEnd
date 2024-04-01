using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private DoorScript door;
    [SerializeField] GameObject player;

    private bool isPickedUp;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            door.UnlockDoor();
            Destroy(gameObject);
        }
    }
}
