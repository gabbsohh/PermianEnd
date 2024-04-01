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
    [SerializeField] int maxLives = 3;
    [SerializeField] int currentLives;

    [SerializeField] private Image healthBarForeground;
    [SerializeField] private TMP_Text livesText;

    public PlayerMovement playerMovement;
    public UIManager uiManager;

    private bool isDead;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    void Awake()
    {
        currentHealth = maxHealth;
        currentLives = PlayerPrefs.GetInt("CurrentLives", maxLives);
        UpdateHealthBar();
        UpdateLivesUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHurt(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        Debug.Log("Player took damage.");

        if(currentHealth <= 0 && currentLives > 0 && !isDead)
        {
            currentLives--;
            isDead = true;
            StartCoroutine(Die());
            //uiManager.GameOver();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
            //UpdateLivesUI();
            PlayerPrefs.SetInt("CurrentLives", currentLives);
            
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("Player has died.");
        // Death Animation for Player goes here.
        // All movement stops, and the player is destroyed afterwards.
        gameObject.GetComponent<PlayerMovement>().StopMovement();
        yield return new WaitForSeconds(1);
        if (currentLives > 0)
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

    void UpdateLivesUI()
    {
        livesText.text = "LIVES: " + currentLives;
    }

    public void CollectHealthCollectable(int healAmount)
    {
        Debug.Log("Health increased by 1");
        Heal(healAmount);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("CurrentLives", currentLives);
    }
}
