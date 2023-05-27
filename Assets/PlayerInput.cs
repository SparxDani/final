using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private ProjectileQueue projectileQueue; // Referencia al script de la cola de proyectiles

    private void Start()
    {
        projectileQueue = GetComponent<ProjectileQueue>(); // Obtener la referencia al script de la cola de proyectiles
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            projectileQueue.LaunchProjectile(); // Lanzar el proyectil al presionar la tecla "O"
        }
    }
}
