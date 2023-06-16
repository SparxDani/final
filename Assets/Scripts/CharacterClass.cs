using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterClass : ScriptableObject
{

    public string characterName;
    public Sprite characterSprite;
    public Sprite characterIcon;


}
