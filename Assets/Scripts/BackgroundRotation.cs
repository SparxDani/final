using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float rotationAngle = rotationSpeed * Time.deltaTime;//Declarar y multiplicar
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);//Declarar, asignar Quaternion que crea una rotación 3D, Se especifica un ángulo de rotación en el eje Z utilizando rotationAngle
        transform.rotation *= rotation;//Actualiza la rotación del objeto al multiplicar su rotación actual por la nueva rotación
    }
}
