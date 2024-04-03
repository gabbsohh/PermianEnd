using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    public float tempSpeed = 0;
    public bool isStunned = false;
    private Rigidbody2D rb;
    private Transform currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        { 
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointB.transform)
        { 
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }

        // Variables for checking if enemy is stunned.
        // Not used in actual stunning function, but for animations.
        if(speed == 0)
        {
            isStunned = true;
        }
        else
        {
            isStunned = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    public void GetStunned()
    {
        tempSpeed = speed;
        StartCoroutine(Stunned(3));
    }

    IEnumerator Stunned(int seconds)
    {   
        speed = 0;
        GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(seconds);
        speed = tempSpeed;
        yield return new WaitForSeconds(1);
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
