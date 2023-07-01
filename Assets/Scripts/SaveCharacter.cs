using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Cargar Personaje", menuName = "Cargas")]
public class SaveCharacter : ScriptableObject
{
    public GameObject playerPrefab;
}
