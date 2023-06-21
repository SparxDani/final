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
        TextMeshProUGUI name = charCell.transform.Find("nameCell").GetComponentInChildren<TextMeshProUGUI>();
        artwork.sprite = character.characterSprite;
        name.text = character.characterName;
        artwork.GetComponent<RectTransform>().pivot = uiPivot(artwork.sprite);
        //artwork.GetComponent<RectTransform>().sizeDelta *= character.zoom;

    }
    public Vector2 uiPivot(Sprite sprite)
    {
        Vector2 pixelSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 pixelPivot = sprite.pivot;
        return new Vector2(pixelPivot.x / pixelSize.x, pixelPivot.y / pixelSize.y);
    }
}
