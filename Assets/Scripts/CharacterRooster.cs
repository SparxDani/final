using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

public class CharacterRooster : MonoBehaviour
{
    private GridLayoutGroup gridLayout;
    [HideInInspector]
    public Vector2 slotArtworkSize;
    public static CharacterRooster instance;

    [Header("Characters List")]
    public SimpleList<CharacterClass> characters = new SimpleList<CharacterClass>();
    [Space]
    [Header("Public References")]
    public GameObject characterCell;
    public GameObject gridBgPrefab;
    public Transform playerSlotsContainer;
    [Space]
    [Header("Current Confirmed Character")]
    public CharacterClass confirmedCharacter;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(gridLayout.cellSize.x * 5, gridLayout.cellSize.y * 2);
        RectTransform gridBG = Instantiate(gridBgPrefab, transform.parent).GetComponent<RectTransform>();
        gridBG.transform.SetSiblingIndex(transform.GetSiblingIndex());
        gridBG.sizeDelta = GetComponent<RectTransform>().sizeDelta;

        slotArtworkSize = playerSlotsContainer.GetChild(0).Find("artwork").GetComponent<RectTransform>().sizeDelta;

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
        Image artwork = charCell.transform.Find("sprite").GetComponent<Image>();
        TextMeshProUGUI name = charCell.transform.Find("nameRect").GetComponentInChildren<TextMeshProUGUI>();
        artwork.sprite = character.characterSprite;
        name.text = character.characterName;
        artwork.GetComponent<RectTransform>().pivot = uiPivot(artwork.sprite);
        artwork.GetComponent<RectTransform>().sizeDelta *= character.zoom;


    }
    public void ShowCharacterInSlot(int player, CharacterClass character)
    {
        bool nullChar = (character == null);

        Sprite artwork = nullChar ? null : character.characterSprite;
        string name = nullChar ? string.Empty : character.characterName;
        string playernickname = "Player " + (player + 1).ToString();
        string playernumber = "P" + (player + 1).ToString();

        Transform slot = playerSlotsContainer.GetChild(player);
        Transform slotArtwork = slot.Find("artwork");
        Transform slotIcon = slot.Find("icon");
        

        if (artwork != null)
        {
            slotArtwork.GetComponent<RectTransform>().pivot = uiPivot(artwork);
            slotArtwork.GetComponent<RectTransform>().sizeDelta = slotArtworkSize;
            slotArtwork.GetComponent<RectTransform>().sizeDelta *= character.zoom;
            slotIcon.GetComponent<Image>().sprite = character.characterIcon;

        }
        slot.Find("artwork").GetComponent<Image>().sprite = artwork;
        slot.Find("name").GetComponent<TextMeshProUGUI>().text = name;
        slot.Find("player").GetComponentInChildren<TextMeshProUGUI>().text = playernickname;
        slot.Find("iconAndPx").GetComponentInChildren<TextMeshProUGUI>().text = playernumber;
    }
  
    public Vector2 uiPivot(Sprite sprite)
    {
        Vector2 pixelSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 pixelPivot = sprite.pivot;
        return new Vector2(pixelPivot.x / pixelSize.x, pixelPivot.y / pixelSize.y);
    }
    public void ConfirmCharacter(int player, CharacterClass character)
    {
        if (confirmedCharacter == null)
        {
            confirmedCharacter = character;
        }
    }

}
