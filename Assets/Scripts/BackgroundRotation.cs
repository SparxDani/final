using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        // Calcular el ángulo de rotación para este frame
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // Crear un quaternion de rotación en el eje Z
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        // Aplicar la rotación al quaternion actual del objeto
        transform.rotation *= rotation;
    }
}
