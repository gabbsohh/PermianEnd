using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Vector2 defaultPos;
    private bool isFalling;

    private Renderer platformRenderer;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float fallDelay;
    [SerializeField] float respawnTime;

    private void Start()
    {
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        platformRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isFalling && other.gameObject.tag == "Player")
        {
            isFalling = true;
            StartCoroutine(PlatformDrop());
        }
    }

    IEnumerator PlatformDrop()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        platformRenderer.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        Reset();
    }

    private void Reset()
    {
        platformRenderer.enabled = true;

        rb.bodyType = RigidbodyType2D.Static;
        transform.position = defaultPos;
        isFalling = false;
    }
}
