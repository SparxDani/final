using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float rotationAngle = rotationSpeed * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        transform.rotation *= rotation;
    }
}
