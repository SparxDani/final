using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBase : MonoBehaviour
{
    //Declaración de variables
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float horizontal;
    private float raycastLenght;
    private bool isJumping = false;
    private bool isFacingRight = true;
    private int jumpsRemaining = 2;

    public float speed = 8f;
    public float JumpingPower = 9.5f;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    public void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);



        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpingPower);
            isJumping = true;
            jumpsRemaining = 1;
        }
        else if (context.performed && jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpingPower);
            isJumping = true;
            jumpsRemaining--;
        }
        else if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position,Vector2.down, raycastLenght, groundLayer);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
            jumpsRemaining = 2;
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
