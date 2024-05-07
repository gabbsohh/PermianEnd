using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;

    public EnemyPatrol enemyPatrol;

    [SerializeField] public bool isArmored;
    [SerializeField] private ParticleSystem deathParticle;

    Animator animator;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if(gameObject.GetComponent<Collider2D>().isActiveAndEnabled == true)
        {
            if(isArmored == false)
            {
                currentHealth -= damage;
            }
        }

        // Hurt Animation for Enemy goes here.
        animator.SetTrigger("hurt");

        if(currentHealth <= 0)
        {
            StartCoroutine(Die());
            deathParticle.Play();
        }
    }

    IEnumerator Die()
    {
        // Disable collision for enemy once it dies.
        gameObject.GetComponent<Collider2D>().enabled = false;
        Debug.Log("Enemy Killed!");
        // Death Animation for Enemy goes here.
        enemyPatrol.speed = 0;
        yield return new WaitForSeconds(1);
        // Enemy gets destroyed once health is depleted.
        gameObject.SetActive(false);
    }

    public void BreakArmor()
    {
        isArmored = false;
    }
}
