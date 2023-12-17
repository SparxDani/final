using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterBase : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Character Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float groundCheckRadius;

    [Header("Character Jumping")]
    [SerializeField] private int totalJumps = 2;
    [SerializeField] private float initialJumpForce = 16;
    [SerializeField] private float airDragMultiplier = 0.95f;
    [SerializeField] private float jumpHeighMultiplier = 0.5f;
    [SerializeField] private float jumpTimerSet = 0.15f;
    [SerializeField] private float jumpTimer;

    private int jumpsLeft;

    [Header("Character Dashin")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCD = 1f;
    private bool isDashing = false;
    private bool isDashCooldown = false;
    private bool canDash = true;

    [Header("Knockback")]
    [SerializeField] private Transform objectCenter;
    [SerializeField] public float knockbackVel = 5f;
    [SerializeField] private float knockbackTime = 1f;
    private bool knockbacked;


    public float baseGravity = 6f;
    //public float maxFallSpeed = 18f;
    public float fallGravityMult = 2f;

    private float movementInput;
    
    private bool attempToJump;
    private bool normalJump;
    private bool bufferedJumpUsable;


    private bool isGrounded;
    private bool canMove;
    public Transform groundCheck;
    public LayerMask whatIsGround;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = totalJumps;

    }
    private void Update()
    {
        if (jumpTimer >= 0)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0)
            {
                canMove = true;
            }
        }
        CheckIfCanJump();
        CheckJump();
        Gravity();
        if (isDashing && !isDashCooldown)
        {
            StartCoroutine(DashCooldown());
        }
    }
    private void FixedUpdate()
    {
        if (!isDashing)
        {
            ApplyMovement();
        }
        CheckSurroundings();

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().x;

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded || totalJumps > 0)
            {
                NormalJump();

            }
            else
            {
                jumpTimer = jumpTimerSet;
                attempToJump = true;
            }
        }
        
        else if ( context.canceled && bufferedJumpUsable && rb.velocity.y > 0)
        {
            bufferedJumpUsable = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeighMultiplier);
            Debug.Log("Buffered");
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && !isDashCooldown)
        {
            StartCoroutine(StartDash());
        }
    }
    public void OnHardFall(InputAction.CallbackContext context)
    {
        /*if(context.performed && canDash)
        {
            StartCoroutine(HardFall());
            Debug.Log("HardFalling");
        }*/

    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        //KnockBack();
    }

    private void ApplyMovement()
    {
        if (!isGrounded && movementInput == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if(canMove)
        {
            rb.velocity = new Vector2(moveSpeed * movementInput, rb.velocity.y);
        }
        else if (knockbacked)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime * 3), rb.velocity.y);
        }
    }
    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * 1.3f;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -initialJumpForce));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    private void NormalJump()
    {
        if (normalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, initialJumpForce);
            jumpsLeft--;
            jumpTimer = 0;
            attempToJump = false;
            bufferedJumpUsable = true;
            Debug.Log("Jump");

        }
    }
    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            jumpsLeft = totalJumps;
        }
        if (jumpsLeft <= 0)
        {
            normalJump = false;
        }
        else
        {
            normalJump = true;
        }
    }
    private void CheckJump()
    {

        if (jumpTimer > 0)
        {
            if (isGrounded)
            {
                NormalJump();
            }
        }
        if (attempToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    public void KnockBack(Transform transform)
    {
        Vector2 knockDirection = objectCenter.position - transform.position;
        knockbacked = true;
        rb.velocity = knockDirection.normalized * knockbackVel;
        StartCoroutine(Unknockback());
    }
    public void UpdateKnockbackForce(float newForce)
    {
        knockbackVel = newForce;
    }
    public IEnumerator Unknockback()
    {
        yield return new WaitForSeconds(knockbackTime);
        knockbacked = false;
    }
    private IEnumerator HardFall()
    {
        canMove = false;
        canDash = false;
        rb.gravityScale = 10;
        rb.velocity = new Vector2(movementInput, 1);
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero;

        canDash = true;
        canMove = true;
        rb.gravityScale = baseGravity;

    }
    private IEnumerator StartDash()
    {
        if (!isDashing && !isDashCooldown)
        {
            isDashing = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(movementInput,0) * dashSpeed;
            yield return new WaitForSeconds(dashDuration);
            rb.velocity = Vector2.zero;
            rb.gravityScale = baseGravity;

            isDashing = false;
            StartCoroutine(DashCooldown());
        }
    }

    private IEnumerator DashCooldown()
    {
        isDashCooldown = true;
        yield return new WaitForSeconds(dashCD);
        isDashCooldown = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}