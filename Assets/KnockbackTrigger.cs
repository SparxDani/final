using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTrigger : MonoBehaviour
{
    [SerializeField] private float knockbackForceIncrement = 3.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterBase player = collision.collider.GetComponent<CharacterBase>();
        if (player != null)
        {
            float currentKnockbackForce = player.GetComponent<CharacterBase>().knockbackVel;
            currentKnockbackForce += knockbackForceIncrement;
            player.UpdateKnockbackForce(currentKnockbackForce);
            player.KnockBack(transform);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
