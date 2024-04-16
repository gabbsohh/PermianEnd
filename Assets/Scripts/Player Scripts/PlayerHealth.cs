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
    [SerializeField] private Rigidbody2D rb;

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
        //healthBarForeground.fillAmount = currentHealth / (float) maxHealth;
        Debug.Log("Player took damage.");
        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Die());
            //uiManager.GameOver();
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("Player has died.");
        // Death Animation for Player goes here.

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

        //healthBarForeground.fillAmount = currentHealth / (float) maxHealth;
    }

}
