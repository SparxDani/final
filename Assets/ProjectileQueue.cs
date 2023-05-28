using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileQueue : 
{
    public GameObject projectilePrefab;
    public float delayBetweenProjectiles = 3f;

    private List<GameObject> projectileList = new List<GameObject>();

    private void Start()
    {
        // Inicializar la lista con 5 proyectiles
        for (int i = 0; i < 5; i++)
        {
            CreateNewProjectile();
        }
    }

    public void LaunchProjectile()
    {
        if (projectileList.Count > 0)
        {
            // Obtener el primer proyectil de la lista
            GameObject projectile = projectileList[0];

            // Eliminar el proyectil de la lista
            projectileList.RemoveAt(0);

            // Lanzar el proyectil
            // Aqu� puedes agregar el c�digo para lanzar el proyectil hacia la direcci�n deseada

            // Esperar el tiempo especificado antes de crear un nuevo proyectil
            StartCoroutine(CreateNewProjectileWithDelay());
        }
        else
        {
            // La lista de proyectiles est� vac�a, no se puede lanzar m�s
            Debug.Log("No hay m�s proyectiles disponibles. Espera a que se recargue.");
        }
    }

    private IEnumerator CreateNewProjectileWithDelay()
    {
        yield return new WaitForSeconds(delayBetweenProjectiles);
        CreateNewProjectile();
    }

    private void CreateNewProjectile()
    {
        // Crear un nuevo proyectil y agregarlo a la lista
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileList.Add(newProjectile);
    }
}
