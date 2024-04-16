using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Image healthBarForeground; 

    public bool isDead;

    public PlayerMovement playerMovement;
    public UIManager uiManager;
    public LifeCounterScript lifeCounter;

    private void Start()
    {
        maxHealth = currentHealth;
        isDead = false;
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void GetHurt(int damage)
    {
        currentHealth -= damage;
        //healthBarForeground.fillAmount = currentHealth / (float) maxHealth;
        Debug.Log("Player took damage.");
        if (currentHealth <= 0)
        {
            lifeCounter.UpdateLives();
            Debug.Log("Player life went down by 1");
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("Player has died.");
        // Death Animation for Player goes here.

        // All movement stops, all collision is removed and the player is destroyed afterwards.
        gameObject.GetComponent<PlayerMovement>().StopMovement();
        yield return new WaitForSeconds(1);
        if (lifeCounter.currentLives > 0)
        {
            currentHealth = maxHealth;
            isDead = false;
            UpdateHealthBar();
        }
        else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
            PlayerPrefs.DeleteKey("CurrentLives");
            Destroy(gameObject);
        }

        // Rigidbody stays in place to prevent player from falling off the map mario-style.
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // Box Collider turns into a trigger to prevent collision with enemies during death.
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        // Change the number here to the duration of the death animation so the whole thing plays out.
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void Heal(int healAmount)
    { 
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //healthBarForeground.fillAmount = currentHealth / (float) maxHealth;
    }

    void UpdateHealthBar()
    {
        healthBarForeground.fillAmount = Mathf.Clamp(currentHealth / (float)maxHealth, 0, 1);
    }

    public void CollectHealthCollectable(int healAmount)
    {
        Debug.Log("Health increased by 1");
        Heal(healAmount);
    }

}
