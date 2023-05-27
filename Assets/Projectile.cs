using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Velocidad del proyectil
    public float lifetime = 1f; // Tiempo de vida del proyectil

    private void Start()
    {
        // Asignar velocidad inicial al proyectil
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        // Destruir el proyectil después de cierto tiempo
        Destroy(gameObject, lifetime);
    }
}
