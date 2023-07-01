using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokerSO : MonoBehaviour
{
    [SerializeField] SaveCharacter invoked;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(invoked.playerPrefab,transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
