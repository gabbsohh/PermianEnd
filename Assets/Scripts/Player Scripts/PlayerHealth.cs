using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;

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
        UpdateHealthBar();

        Debug.Log("Player took damage.");
        if (currentHealth <= 0)
        {
            lifeCounter.UpdateLives();
            Debug.Log("Player life went down by 1");
        }
    }

    void UpdateHealthBar()
    {
        healthBarForeground.fillAmount = Mathf.Clamp(currentHealth / (float)maxHealth, 0, 1);
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void CollectHealthCollectable(int healAmount)
    {
        Debug.Log("Health increased by 1");
        Heal(healAmount);
    }

}
