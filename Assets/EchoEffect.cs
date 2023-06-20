using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public float timeBtwSpawns;
    public float startTime;
    public GameObject echo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwSpawns <= 0)
        {
            GameObject temp = Instantiate(echo, transform.position, Quaternion.identity);
            timeBtwSpawns = startTime;
            Destroy(temp, 0.5f);
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
