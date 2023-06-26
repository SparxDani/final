using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Nuevo Personaje", menuName = "Personaje")]
public class CharacterClass : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public Sprite characterIcon;
    public float zoom = 2;
}
