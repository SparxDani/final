using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileQueue : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float delayBetweenProjectiles = 3f;

    private List<GameObject> projectileList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            CreateNewProjectile();
        }
    }

    public void LaunchProjectile()
    {
        if (projectileList.Count > 0)
        {
            GameObject projectile = projectileList[0];

            projectileList.RemoveAt(0);

            StartCoroutine(CreateNewProjectileWithDelay());
        }
        else
        {
            Debug.Log("No hay más proyectiles disponibles. Espera a que se recargue.");
        }
    }

    private IEnumerator CreateNewProjectileWithDelay()
    {
        yield return new WaitForSeconds(delayBetweenProjectiles);
        CreateNewProjectile();
    }

    private void CreateNewProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileList.Add(newProjectile);
    }
}
