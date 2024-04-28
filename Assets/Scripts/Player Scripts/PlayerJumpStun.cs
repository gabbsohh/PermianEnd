using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStun : MonoBehaviour
{
    public float bounce;
    public Rigidbody2D rb2d;

    [SerializeField] private AudioClip jumpStunSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if enemy is being jumped on.

        if(other.CompareTag("Enemy"))
        {
            // Check if enemy's box collider is a trigger to prevent repeat stunning.
            // This is so the tempSpeed variable of EnemyPatrol doesn't drop to 0,
            // permanently lowering the enemy's speed.

            if(other.GetComponent<BoxCollider2D>().isTrigger == false)
            {
                // Stun the enemy once they're jumped on.
                Debug.Log("Enemy Stunned!");
                // Call the stun function of EnemyPatrol.
                other.gameObject.GetComponent<EnemyPatrol>().GetStunned(3f);
                AudioManager.instance.PlaySoundFXClip(jumpStunSoundClip, transform, 0.5f);
                // Make the player bounce off of the enemy.
                rb2d.velocity = new Vector2(rb2d.velocity.x, bounce);
            }
        }
    }
}
