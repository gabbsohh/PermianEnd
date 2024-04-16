using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator animator;

    public float attackRate = 0.5f;
    public float canAttack = -1f;

    public Transform attackPoint;
    public float attackArea = 0.75f;
    public LayerMask enemyLayers;

    public int attackDamage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && Time.time > canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Scripts For Animating Goes Here

        // Scripts For Attack Functionality
        Debug.Log("Player Attacked.");

        // Creates an array of everything hit by the attack, then damages them.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackArea, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.name + " hit.");
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }


        canAttack = Time.time + attackRate;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackArea);
    }
}
