using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(gameObject.GetComponent<Collider2D>().isActiveAndEnabled == true)
        {
            currentHealth -= damage;
        }

        // Hurt Animation for Enemy goes here.

        if(currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        // Disable collision for enemy once it dies.
        gameObject.GetComponent<Collider2D>().enabled = false;
        Debug.Log("Enemy Killed!");
        // Death Animation for Enemy goes here.
        yield return new WaitForSeconds(1);
        // Enemy gets destroyed once health is depleted.
        gameObject.SetActive(false);
    }
}
