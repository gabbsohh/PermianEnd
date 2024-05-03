using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] public int currentHealth;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Image healthBarForeground; 

    public bool isDead;

    public PlayerMovement playerMovement;
    public UIManager uiManager;

    private void Start()
    {
        currentHealth = maxHealth;
        GameData.health = maxHealth;
        isDead = false;
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void GetHurt(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        GameData.health = currentHealth;
        Debug.Log("Player took damage.");
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public IEnumerator Die()
    {
        Debug.Log("Player has died.");
        // death anim

        // All movement stops, all collision is removed and the player is destroyed afterwards.
        gameObject.GetComponent<PlayerMovement>().StopMovement();

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
        GameData.health = currentHealth;
    }

    void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            float fillAmount = Mathf.Clamp(currentHealth / (float)maxHealth, 0, 1);
            healthBarForeground.fillAmount = fillAmount;
        }
        else
        {
            Debug.LogError("Health bar foreground image is not assigned!");
        }
    }

    public void CollectHealthCollectable(int healAmount)
    {
        Debug.Log("Health increased by 1");
        Heal(healAmount);
    }

}
