using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 5;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Hurt Animation for Enemy goes here.

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Killed!");

        // Death Animation for Enemy goes here.
        // (Reminder to add a timer after the animation, so it can actually play before the enemy gets destroyed).

        // Enemy gets destroyed once health is depleted.
        gameObject.SetActive(false);
    }
}
