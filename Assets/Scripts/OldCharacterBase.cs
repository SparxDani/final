using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class OldCharacterBase : MonoBehaviour
{
    [Header("Inputs Settings")]
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private bool snapInput = true;
    [SerializeField] private float VdeadZoneLimit = 0.5f;
    [SerializeField] private float HdeadZoneLimit = 0.1f;

    private Rigidbody2D rb;
    private float time;
    private CapsuleCollider2D capCol;
    private Vector2 frameVelocity;
    private Vector2 move;

    private PlayerInput input = null;



    [Header("Character Movement")]
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float groundDeceleration = 50;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float airDeceleration = 20;
    [SerializeField] private float groundedForce = -2f;
    [SerializeField] private float groundDistanceDetection = 0.10f;

    private float movementInput;

    private bool isGrounded;
    private bool queryColliders;
    private float frameLeftGrounded = float.MinValue;


    [Header("Character Jumping")]
    [SerializeField] private float initialJumpForce = 32;
    [SerializeField] private int totalJumps = 2;
    [SerializeField] private float airDragMultiplier = 0.95f;
    [SerializeField] private int jumpsLeft;
    [SerializeField] private float fallingMaxSpeed;
    [SerializeField] private float fallingAccel = 0.95f;
    [SerializeField] private float endJumpEarlyModifier;
    [SerializeField] private float CoyoteJump;
    [SerializeField] private float BufferJump;
    [SerializeField] private float jumpTimerSet = 0.20f;

    private bool coyoteUsable;
    private bool normalJump;
    private bool canMove;
    private float jumpTimer;

    private bool attempToJump;
    private bool jumpToConsume;
    private bool bufferedJumpUsable;
    private bool endedJumpEarly;

    private bool jumpHeld;


    private bool HasBufferedJump => bufferedJumpUsable && time < jumpTimer + BufferJump;
    private bool CanUseCoyote => coyoteUsable && !isGrounded && time < frameLeftGrounded + CoyoteJump;

    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;


    public void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        capCol = GetComponent<CapsuleCollider2D>();
        queryColliders = Physics2D.queriesStartInColliders;
        jumpsLeft = totalJumps;

    }

    private void Update()
    {
        time += Time.deltaTime;
        
        if (snapInput)
        {
            move.x = Mathf.Abs(move.x) < HdeadZoneLimit ? 0 : Mathf.Sign(move.x);
            move.y = Mathf.Abs(move.y) < VdeadZoneLimit ? 0 : Mathf.Sign(move.y);
        }

        if (normalJump == true)
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

        if(jumpTimer >= 0)
        {
            jumpTimer -= Time.deltaTime;
            if(jumpTimer <= 0)
            {
                canMove = true;
            }
        }

        if (bufferedJumpUsable && !normalJump)
        {
            bufferedJumpUsable = false;
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * fallingAccel);
        }
        CheckIfCanJump();
        CheckJump();

        /*if (jumpDown)
        {
            jumpToConsume = true;
            timeJumpWasPressed = time;
        }*/
        ApplyMovement();

    }
    public void FixedUpdate()
    {
        //ApplyMovement();
        //Gravity();
        //Collisions();
        //HandleJump();
        

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().x;

    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
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

        else if (context.canceled && bufferedJumpUsable && rb.velocity.y > 0)
        {
            bufferedJumpUsable = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallingAccel);
            Debug.Log("Buffered");
        }
    }
    

    private void Collisions()
    {
        Physics2D.queriesStartInColliders = false;
        bool groundHit = Physics2D.CapsuleCast(capCol.bounds.center, capCol.size, capCol.direction, 0, Vector2.down, groundDistanceDetection, ~PlayerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(capCol.bounds.center, capCol.size, capCol.direction, 0, Vector2.up, groundDistanceDetection, ~PlayerLayer);

        if (ceilingHit)
        {
            frameVelocity.y = Mathf.Min(0, frameVelocity.y);
        }

        if (!isGrounded && groundHit)
        {
            isGrounded = true;
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;
            GroundedChanged?.Invoke(true, Mathf.Abs(frameVelocity.y));


        }
        else if (isGrounded && !groundHit)
        {
            isGrounded = false;
            frameLeftGrounded = time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = queryColliders;

    }
    private void ApplyMovement()
    {
        move = rb.velocity;

        if (!isGrounded && movementInput == 0)
        {
            float decelerate = isGrounded ? groundDeceleration : airDeceleration;
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier * decelerate, rb.velocity.y);
        }
        else if (canMove)
        {
            rb.velocity = new Vector2(moveSpeed * movementInput, rb.velocity.y);
        }
        
    }
    public void Direction(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        
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
            //Jumped?.Invoke();
            Debug.Log("Jump");

        }
        /*endedJumpEarly = false;
        timeJumpWasPressed = 0;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = initialJumpForce;*/
    }
    private void CheckJump()
    {
        
        if (jumpTimer > 0)
        {
            if (isGrounded || CanUseCoyote)
            {
                NormalJump();
            }
        }
        if (attempToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
        
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            jumpsLeft = totalJumps;
        }
        if(jumpsLeft <= 0)
        {
            normalJump = false;
        }
        else
        {
            normalJump = true;
        }
    }
    private void Gravity()
    {
        if (isGrounded && frameVelocity.y <= 0f)
        {
            frameVelocity.y = groundedForce;
        }
        else
        {
            float gravityInAir = fallingAccel;
            if (endedJumpEarly && frameVelocity.y > 0)
            {
                gravityInAir *= endJumpEarlyModifier;
            }
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -fallingMaxSpeed, gravityInAir * Time.fixedDeltaTime);
        }
    }
}
