using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBase : MonoBehaviour, IPlayerController
{
    [Header("Inputs Settings")]
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private bool snapInput = true; 
    [SerializeField] private float VdeadZoneLimit = 0.5f;
    [SerializeField] private float HdeadZoneLimit = 0.1f;
    
    private Rigidbody2D rb;
    private float time;
    private CapsuleCollider2D capCol;
    private Inputs _inputs;
    private Vector2 frameVelocity;
    private Players controls;
    


    [Header("Character Movement")]
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float acceleration = 100;
    [SerializeField] private float groundDeceleration = 50;
    [SerializeField] private float airDeceleration = 20;
    [SerializeField] private float groundedForce = -2f;
    [SerializeField] private float groundDistanceDetection = 0.10f;

    private bool grounded;
    private bool queryColliders;
    private float frameLeftGrounded = float.MinValue;


    [Header("Character Jumping")]
    [SerializeField] private float initialJumpForce = 32;
    [SerializeField] private float fallingMaxSpeed;
    [SerializeField] private float fallingAccel = 98.7f;
    [SerializeField] private float endJumpEarlyModifier;
    [SerializeField] private float CoyoteJump;
    [SerializeField] private float BufferJump;

    private bool coyoteUsable;
    private float timeJumpWasPressed;
    private bool jumpToConsume;
    private bool bufferedJumpUsable;
    private bool endedJumpEarly;


    private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + BufferJump;
    private bool CanUseCoyote => coyoteUsable && !grounded && time < frameLeftGrounded + CoyoteJump;

    public Vector2 Inputs => _inputs.movement;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capCol = GetComponent<CapsuleCollider2D>();
        queryColliders = Physics2D.queriesStartInColliders;

        controls = new Players();
        controls.Enable();
    }

    private void Update()
    {
        time += Time.deltaTime;
        ReadInput();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void ReadInput()
    {
        _inputs = new Inputs
        {
            jumpDown = controls.Ingame.Jump.triggered,
            jumpHeld = controls.Ingame.Jump.ReadValue<float>() > 0.1f,
            movement = controls.Ingame.Movement.ReadValue<Vector2>()
        };

        if (snapInput)
        {
            _inputs.movement.x = Mathf.Abs(_inputs.movement.x) < HdeadZoneLimit ? 0 : Mathf.Sign(_inputs.movement.x);
            _inputs.movement.y = Mathf.Abs(_inputs.movement.y) < VdeadZoneLimit ? 0 : Mathf.Sign(_inputs.movement.y);
        }
        if (_inputs.jumpDown)
        {
            jumpToConsume = true;
            timeJumpWasPressed = time;
        }

    }
    public void FixedUpdate()
    {
        Collisions();

        HandleJump();
        Direction();
        Gravity();

        Move();
    }

    private void Collisions()
    {
        Physics2D.queriesStartInColliders = false;
        bool groundHit = Physics2D.CapsuleCast(capCol.bounds.center, capCol.size, capCol.direction, 0, Vector2.down, groundDistanceDetection, ~PlayerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(capCol.bounds.center, capCol.size, capCol.direction, 0, Vector2.up, groundDistanceDetection, ~PlayerLayer);

        if(ceilingHit)
        {
            frameVelocity.y = Mathf.Min(0, frameVelocity.y);
        }

        if (!grounded && groundHit)
        {
            grounded = true;
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;                                                                 
            GroundedChanged?.Invoke(true, Mathf.Abs(frameVelocity.y));


        }
        else if (grounded && !groundHit)
        {
            grounded = false;
            frameLeftGrounded = time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = queryColliders;

    }

    


    private void HandleJump()
    {
        if (!endedJumpEarly && !grounded && !_inputs.jumpHeld && rb.velocity.y > 0)
        {
            endedJumpEarly = true;
        }

        if (!jumpToConsume && !HasBufferedJump)
        {
            return;
        }

        if (grounded || CanUseCoyote)
        {
            ExecuteJump();
        }

        jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        endedJumpEarly = false;
        timeJumpWasPressed = 0;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = initialJumpForce;
        Jumped?.Invoke();
    }
    private void Direction()
    {
        if (_inputs.movement.x == 0)
        {
            var decelerate = grounded ? groundDeceleration : airDeceleration;
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, decelerate * Time.fixedDeltaTime);
        }
        else
        {
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, _inputs.movement.x * maxSpeed, acceleration * Time.fixedDeltaTime);
        }
    }
    private void Gravity()
    {
        if (grounded && frameVelocity.y <= 0f)
        {
            frameVelocity.y = groundedForce;
        }
        else
        {
            var gravityInAir = fallingAccel;
            if (endedJumpEarly && frameVelocity.y > 0)
            {
                gravityInAir *= endJumpEarlyModifier;
            }
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -fallingMaxSpeed, gravityInAir * Time.fixedDeltaTime);
        }
    }
    private void Move()
    {
        rb.velocity = frameVelocity;
    }

}
public struct Inputs
{
    public bool jumpDown;
    public bool jumpHeld;
    public Vector2 movement;
}
public interface IPlayerController
{
    public Vector2 Inputs { get; }
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

}