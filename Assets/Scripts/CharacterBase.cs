using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Transform groundCheck;
    protected LayerMask groundLayer;

    protected float horizontal;
    protected bool isJumping = false;
    protected bool isFacingRight = true;
    protected int jumpsRemaining = 2;

    [SerializeField] protected float speed = 8f;
    [SerializeField] protected float jumpingPower = 9.5f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
    }

    protected virtual void Update()
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

    public virtual void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping = true;
            jumpsRemaining = 1;
        }
        else if (context.performed && jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping = true;
            jumpsRemaining--;
        }
        else if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    protected virtual bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
            jumpsRemaining = 2;
        }
    }

    protected virtual void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public virtual void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
