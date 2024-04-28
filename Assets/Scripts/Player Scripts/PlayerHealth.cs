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

    [SerializeField] private AudioClip hurtSoundClip;

    [SerializeField] private Image healthBarForeground; 

    public bool isDead;

    public PlayerMovement playerMovement;
    public UIManager uiManager;
    public LifeCounterScript lifeCounter;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numFlashes;
    private SpriteRenderer spriteRend;

    public GameObject StompChecker;

    private void Start()
    {
        maxHealth = currentHealth;
        isDead = false;
        spriteRend = GetComponent<SpriteRenderer>();
        StompChecker = transform.GetChild(2).gameObject;
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
        AudioManager.instance.PlaySoundFXClip(hurtSoundClip, transform, 0.5f);
        if (currentHealth <= 0)
        {
            lifeCounter.UpdateLives();
            Debug.Log("Player life went down by 1");
        }
        else
        {
            StartCoroutine(Invulnerability());
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

    private IEnumerator Invulnerability()
    {
        // Following script causes Player to ignore enemy collision after getting hurt, and disables the jump stun temporarily.
        Physics2D.IgnoreLayerCollision(9,6,true);
        StompChecker.GetComponent<BoxCollider2D>().enabled = false;
        for(int i = 0; i < numFlashes; i++)
        {
            spriteRend.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(1);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(1);
        }
        StompChecker.GetComponent<BoxCollider2D>().enabled = true;
        Physics2D.IgnoreLayerCollision(9,6,false);
    }
}
