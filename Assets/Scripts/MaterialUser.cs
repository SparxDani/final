using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialUser : MonoBehaviour
{
    public CustomMaterial cubeConfig;

    private Renderer cubeRenderer;

    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();

        cubeRenderer.material = new Material(cubeConfig.customShader);
        cubeRenderer.material.SetFloat("_Opacity", cubeConfig.opacity);
        cubeRenderer.material.SetColor("_CustomColor", cubeConfig.customColor);
    }
}

