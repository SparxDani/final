using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb2d;
    public int punchForce = 0;
    public int hit = 0;
    [SerializeField] CollisionEnemy[] colisiones;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (punchForce >= 3 && colisiones[0].colision == true)
        {
            rb2d.velocity = new Vector2(hit, hit) * 0.3f;
            punchForce = 0;
            print("activedLeft");
        }
        else if (punchForce >= 3 && colisiones[1].colision == true)
        {
            rb2d.velocity = new Vector2(-hit, hit) * 0.3f;
            punchForce = 0;
            print("activedRIGHT");

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            punchForce++;
            hit++;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Distance"))
        {
            punchForce = 0;
            print("hola");
        }
    }
}
