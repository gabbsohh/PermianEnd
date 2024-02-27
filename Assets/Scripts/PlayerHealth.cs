using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GetHurt(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage.");
        if(currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("Player has died.");
        // Death Animation for Player goes here.
        // All movement stops, and the player is destroyed afterwards.
        gameObject.GetComponent<PlayerMovement>().StopMovement();
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
