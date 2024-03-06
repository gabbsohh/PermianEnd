using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;

    [SerializeField] private Image healthBarForeground; 

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHurt(int damage)
    {
        currentHealth -= damage;
        healthBarForeground.fillAmount = currentHealth / (float) maxHealth;
        Debug.Log("Player took damage.");
        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Die());
            uiManager.GameOver();
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("Player has died.");
        // Death Animation for Player goes here.
        // All movement stops, and the player is destroyed afterwards.
        gameObject.GetComponent<PlayerMovement>().StopMovement();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void Heal(int healAmount)
    { 
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBarForeground.fillAmount = currentHealth / (float) maxHealth;
    }

}
