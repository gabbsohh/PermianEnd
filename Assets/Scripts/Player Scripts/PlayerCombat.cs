using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public float attackRate = 0.5f;
    public float canAttack = -1f;
    public float canSwitch = -1f;

    public Transform attackPoint;
    public float attackArea = 0.75f;
    public LayerMask enemyLayers;
    public LayerMask obstacleLayers;

    public int attackDamage = 1;

    public List<string> weapons;
    string currentWeapon = "";
    int weaponIndex = 0;

    public bool usingPick;

    // Start is called before the first frame update
    public void Start()
    {
        // Adds the default weapon to the weapon list.
        weapons.Add("Blade");
        currentWeapon = weapons[0];

        // Get Animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if button is being pressed to perform certain actions, with a cooldown to prevent spam.
        if(Input.GetKeyDown(KeyCode.Q) && Time.time > canAttack)
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.E) && Time.time > canSwitch)
        {
            SwitchWeapon();
        }

        // Checks the player's current weapon and adjusts stats accordingly.
        if(currentWeapon == "Blade")
        {
            attackDamage = 1;
            attackRate = 0.35f;
            usingPick = false;
            // Use animation set for blade.
            animator.SetInteger("weaponCounter", 0);
        }

        if(currentWeapon == "Pickaxe")
        {
            attackDamage = 2;
            attackRate = 0.5f;
            usingPick = true;
            // Use animation set for pickaxe.
            animator.SetInteger("weaponCounter", 1);
            //animator.SetBool("isAttacking", true);
        }
    }

    void SwitchWeapon()
    {
        // Checks to see if the amount of weapons in the list is more than 1 to warrant switching.
        if(weapons.Count > 1)
        {
            // Weapon Index Increases whenever weapon switch is called, going back to the first position if it happens to reach the end.
            weaponIndex++;
            if(weaponIndex >= weapons.Count)
            {
                weaponIndex = 0;
            }
            currentWeapon = weapons[weaponIndex];
            Debug.Log("Weapon switched to " + currentWeapon + ".");
        }
        else
        Debug.Log("Not enough weapons to switch between!");
    }

    void Attack()
    {
        // Scripts For Animating Goes Here
        animator.SetBool("isAttacking", true);

        // Scripts For Attack Functionality
        Debug.Log("Player Attacked.");

        // Creates an array of everything hit by the attack, then damages them.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackArea, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<EnemyHealth>().isArmored == false)
            {
                Debug.Log(enemy.name + " hit.");
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
            if(enemy.GetComponent<EnemyHealth>().isArmored == true && usingPick == false)
            {
                Debug.Log("Enemy armor resisted damage!");
            }
            if(enemy.GetComponent<EnemyHealth>().isArmored == true && usingPick == true)
            {
                enemy.GetComponent<EnemyHealth>().isArmored = false;
                Debug.Log("Enemy armor was broken!");
            }
        }

        Collider2D[] destroyedObstacles = Physics2D.OverlapCircleAll(attackPoint.position, attackArea, obstacleLayers);
        foreach(Collider2D obstacle in destroyedObstacles)
        {
            if(usingPick == true)
            {
                obstacle.GetComponent<BreakableBlockScript>().BreakBlock();
            }
        }

        canAttack = Time.time + attackRate;

        StartCoroutine(ResetAttackAnimation());
    }

    IEnumerator ResetAttackAnimation()
    {
        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(attackRate);

        // Reset isActive parameter to false
        animator.SetBool("isAttacking", false);
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
