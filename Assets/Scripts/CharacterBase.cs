using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static CharacterBase;

public class CharacterBase : MonoBehaviour, IPlayerController
{
    //Declaración de variables
    public Rigidbody2D rb;
    //public Transform groundCheck;
    //public LayerMask groundLayer;

    /*private float raycastLenght;
    private bool isJumping = false;
    private bool isFacingRight = true;
    private int jumpsRemaining = 2;
    //public InputManagerController manager;
    public float JumpingPower = 9.5f;
    public float horizontal;*/


    private float time;
    private CapsuleCollider2D capCol;

    //Inputs
    private Inputs _inputs;
    private bool snapInput = true;
    private Vector2 frameVelocity;
    private Players controls;

    //Movement
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float acceleration = 100;
    [SerializeField] private float VdeadZoneLimit = 0.5f;
    [SerializeField] private float HdeadZoneLimit = 0.1f;
    [SerializeField] private bool grounded;
    [SerializeField] private float groundDeceleration = 50;
    [SerializeField] private float airDeceleration = 20;
    private bool queryColliders;

    public Vector2 Inputs => _inputs.movement;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new Players();
        controls.Enable();
        queryColliders = Physics2D.queriesStartInColliders;
    }

    private void Update()
    {
        time = Time.deltaTime;
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
            jumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            jumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
            movement = controls.Ingame.Movement.ReadValue<Vector2>()
        };

        if (snapInput)
        {
            _inputs.movement.x = Mathf.Abs(_inputs.movement.x) < HdeadZoneLimit ? 0 : Mathf.Sign(_inputs.movement.x);
            _inputs.movement.y = Mathf.Abs(_inputs.movement.y) < VdeadZoneLimit ? 0 : Mathf.Sign(_inputs.movement.y);
        }

    }
    public void FixedUpdate()
    {
        Direction();
        Move();
    }
    private void Direction()
    {
        if(_inputs.movement.x == 0)
        {
            var decelerate = grounded ? groundDeceleration : airDeceleration;
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, Time.deltaTime);
        }
        else
        {
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, _inputs.movement.x * maxSpeed, acceleration * Time.fixedDeltaTime);
        }
    }

    public void Move()
    {
        rb.velocity = frameVelocity;
    }
    /*private void OnEnable()
    {
        movementInput.Enable();
        movementInput.Ingame.Movement.performed += Movement_performed;
        movementInput.Ingame.Movement.canceled += Movement_canceled;
    }

    private void OnDisable()
    {
        movementInput.Disable();
        movementInput.Ingame.Movement.performed -= Movement_performed;
        movementInput.Ingame.Movement.canceled -= Movement_canceled;
    }

    private void Movement_canceled(InputAction.CallbackContext obj)
    {
        //movementMap = Vector2.zero;
        movementMap = obj.ReadValue<Vector2>();
        //horizontal = context.ReadValue<Vector2>().x;
        if (movementMap != null ) { }
    }

    private void Movement_performed(InputAction.CallbackContext obj)
    {
        movementMap = obj.ReadValue<Vector2>();
    }*/




    /*public void Jump(InputAction.CallbackContext context)
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
        return Physics2D.Raycast(groundCheck.position, Vector2.down, raycastLenght, groundLayer);
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
    */
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
}