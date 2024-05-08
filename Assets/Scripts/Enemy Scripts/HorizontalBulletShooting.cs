using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBulletShooting : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private Rigidbody2D rb;
    //control speed of bullet
    public float force;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        /*Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Rotates bullet to be more accurate looking
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);*/

        rb.velocity = Vector2.right * force;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        Vector3 direction = enemy.transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // destroy bullet
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When collide with player, bullet destroys self. Include ground collision, maybe?
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().currentHealth -= 1;
            Destroy(gameObject);
        }
    }
}
