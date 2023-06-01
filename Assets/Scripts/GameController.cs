using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    SimplyLinkList<GameObject> bullet;
    [SerializeField] GameObject bulletPrefab;
    private void Awake()
    {
        bullet = new SimplyLinkList<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            bullet.AddNodeAtStart(bulletPrefab);
        }
    }
    void Start()
    {
        GameObject newProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.AddNodeAtStart(newProjectile);
        bullet.PrintAllNodes();
    }
    private void CreateNewProjectile()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
