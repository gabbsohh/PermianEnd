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

    public PlayerMovement playerMovement;
    public UIManager uiManager;
    public LifeCounterScript lifeCounter;

    private bool isDead;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        lifeCounter.UpdateLivesUI();
    }

    public void GetHurt(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        Debug.Log("Player took damage.");

        if(currentHealth <= 0 && lifeCounter.currentLives > 0 && !isDead)
        {
            lifeCounter.currentLives--;
            isDead = true;
            StartCoroutine(Die());
            //uiManager.GameOver();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
            //UpdateLivesUI();
            PlayerPrefs.SetInt("CurrentLives", lifeCounter.currentLives);
            
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("Player has died.");
        // Death Animation for Player goes here.
        // All movement stops, and the player is destroyed afterwards.
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
    }

    public void Heal(int healAmount)
    { 
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBarForeground.fillAmount = currentHealth / (float) maxHealth;
    }

    public void CollectHealthCollectable(int healAmount)
    {
        Debug.Log("Health increased by 1");
        Heal(healAmount);
    }

}
