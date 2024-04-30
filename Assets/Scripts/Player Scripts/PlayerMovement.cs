using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] public float speed = 6.5f;
    [SerializeField] public float jump = 10f;
    private float tempSpeed = 0f;
    private float tempJump = 0f;
    [SerializeField] private AudioClip jumpSoundClip;
    private bool isFacingRight = true;
    PlayerHealth pHealth;

    private bool canDoubleJump;

    private bool canFlip = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        pHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pHealth.isDead)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (IsGrounded() && !Input.GetKey(KeyCode.Space))
            {
                canDoubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGrounded())
                {
                    //rb.velocity = new Vector2(rb.velocity.x, jump);
                    rb.velocity = Vector2.up * jump;
                    AudioManager.instance.PlaySoundFXClip(jumpSoundClip, transform, 0.5f);
                    Debug.Log("Jumping!");
                    //doubleJump = !doubleJump;
                }
                else
                {
                    if(canDoubleJump)
                    {
                        rb.velocity = Vector2.up * jump;
                        AudioManager.instance.PlaySoundFXClip(jumpSoundClip, transform, 0.5f);
                        Debug.Log("Double Jumping!");
                        canDoubleJump = false;
                    }
                }
            }

            if(canFlip == true)
            {
                Flip();
            }
        }
    
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        { 
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void StopMovement()
    {
        tempSpeed = speed;
        tempJump = jump;
        speed = 0f;
        jump = 0f;
        canFlip = false;
        Physics2D.IgnoreLayerCollision(9,6,true);
    }

    public void ResumeMovement()
    {
        speed = tempSpeed;
        jump = tempJump;
        canFlip = true;
        Physics2D.IgnoreLayerCollision(9,6,false);
    }
}
