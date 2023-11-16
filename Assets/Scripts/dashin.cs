using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashin : MonoBehaviour
{
    private Rigidbody2D rb;
    private CharacterBase character;
    private float baseGravity;
    private TrailRenderer trailRenderer;


    [SerializeField] private float dashTime;
    [SerializeField] private float dashForce;
    [SerializeField] private float delay;
    public bool IsDashing;
    private bool canDash;
    public bool _IsDashing => IsDashing;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<CharacterBase>();
        baseGravity = rb.gravityScale;
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            //StartCoroutine(Dash());
        }
    }

    /*private IEnumerator Dash()
    {
        if (character.horizontal != 0)
        {
            IsDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(character.horizontal * dashForce * 2, 0f);

            trailRenderer.enabled = true; 

            yield return new WaitForSeconds(dashTime);

            IsDashing = false;
            rb.gravityScale = baseGravity;

            trailRenderer.enabled = false; 

            yield return new WaitForSeconds(delay);
            canDash = true;
        }
    }*/

}
