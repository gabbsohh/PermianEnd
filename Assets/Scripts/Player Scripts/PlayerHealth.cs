using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    [SerializeField] public int currentHealth;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private AudioClip hurtSoundClip;

    [SerializeField] private Image healthBarForeground; 

    [SerializeField] public Vector3 respawnPoint;

    public bool isDead;

    public PlayerMovement playerMovement;
    public HealthUI healthUI;
    public UIManager uiManager;
    //public LifeCounterScript lifeCounter;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numFlashes;
    private SpriteRenderer spriteRend;

    public GameObject StompChecker;

    private void Start()
    {
        currentHealth = maxHealth;
        GameData.health = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
        isDead = false;
        spriteRend = GetComponent<SpriteRenderer>();
        StompChecker = transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        
    }

    public void GetHurt(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        GameData.health = currentHealth;
        Debug.Log("Player took damage.");
        AudioManager.instance.PlaySoundFXClip(hurtSoundClip, transform, 0.5f);
        if (currentHealth <= 0)
        {
            //lifeCounter.UpdateLives();
            Debug.Log("Player's lives went down by 1");
            StartCoroutine(Die());
            isDead = true;
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
        // Uses the player's movement script reduce their speed and jump so they can't move.
        gameObject.GetComponent<PlayerMovement>().StopMovement();
        yield return new WaitForSeconds(1);
        //if (lifeCounter.currentLives > 0)
        //{
        //    currentHealth = maxHealth;
        //    UpdateHealthBar();
        //}
        //else 
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        //    PlayerPrefs.DeleteKey("CurrentLives");
        //}

        // Change the number here to the duration of the death animation so the whole thing plays out.
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Respawn();
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        isDead = false;
        gameObject.GetComponent<PlayerMovement>().ResumeMovement();
    }

    public void Heal(int healAmount)
    { 
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        GameData.health = currentHealth;
        healthUI.UpdateHearts(currentHealth);
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

    public void Respawn()
    {
        transform.position = respawnPoint;
    }
}
