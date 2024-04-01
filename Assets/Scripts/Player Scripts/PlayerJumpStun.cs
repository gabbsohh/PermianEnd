using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStun : MonoBehaviour
{
    public float bounce;
    public Rigidbody2D rb2d;

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
        if(other.CompareTag("Enemy"))
        {
            // Stun the enemy once they're jumped on.
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            Debug.Log("Enemy Stunned!");
            StartCoroutine(StunEnemy());
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("Enemy is no longer Stunned!");

            // Make the player bounce off of the enemy.
            rb2d.velocity = new Vector2(rb2d.velocity.x, bounce);
        }
    }

    IEnumerator StunEnemy()
    {
        yield return new WaitForSeconds(8f);
    }
}
