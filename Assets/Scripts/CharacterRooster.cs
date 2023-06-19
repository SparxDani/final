using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.TextCore.Text;

public class CharacterRooster : MonoBehaviour
{
    public SimplyLinkedList<CharacterClass> characters = new SimplyLinkedList<CharacterClass>();
    public GameObject characterCell;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            CharacterClass character = characters.Get(i);
            SpawnCharacterCell(character);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnCharacterCell(CharacterClass character)
    {
        GameObject charCell = Instantiate(characterCell, transform);
        charCell.name = character.characterName;
        Image artwork = charCell.transform.Find("artwork").GetComponent<Image>();
        TextMeshProUGUI name = charCell.transform.Find("nameRect").GetComponentInChildren<TextMeshProUGUI>();
        artwork.sprite = character.characterSprite;
        name.text = character.characterName;

        
    }
}
