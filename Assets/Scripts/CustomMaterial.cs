using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeConfiguration", menuName = "Custom/Cube Configuration")]
public class CustomMaterial : ScriptableObject
{
    public Shader customShader;
    public float opacity;
    public Color customColor;
}
