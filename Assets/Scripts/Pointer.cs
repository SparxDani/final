using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    private GraphicRaycaster graphicRay;
    private PointerEventData pointerEventData = new PointerEventData(null);
    public Transform currentCharacter;

    public Transform token;
    public bool hasToken;
    public SaveCharacter carga;

    void Start()
    {
        graphicRay = GetComponentInParent<GraphicRaycaster>();
        CharacterRooster.instance.ShowCharacterInSlot(0, null);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentCharacter != null)
            {
                TokenFollow(false);
                CharacterRooster.instance.ConfirmCharacter(0, CharacterRooster.instance.characters[currentCharacter.GetSiblingIndex()]);
                //carga.playerPrefab = graphicRay.gameObject;

                print("Seleccionado");

            }
        }

        
        if (Input.GetKeyDown(KeyCode.O))
        {
            CharacterRooster.instance.confirmedCharacter = null;
            TokenFollow(true);
        }

        if (hasToken)
        {
            token.position = transform.position;
        }
        pointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRay.Raycast(pointerEventData, results);
        if (hasToken)
        {

            if (results.Count > 0)
            {
                Transform raycastCharacter = results[0].gameObject.transform;

                if (raycastCharacter != currentCharacter)
                {
                    if (currentCharacter != null)
                    {
                        currentCharacter.Find("selectedBorder").GetComponent<Image>().color = Color.clear;
                    }
                    SetCurrentCharacter(raycastCharacter);
                }
            }
            else
            {
                if (currentCharacter != null)
                {
                    currentCharacter.Find("selectedBorder").GetComponent<Image>().color = Color.clear;
                    SetCurrentCharacter(null);
                }
            }
        }
    }

    void SetCurrentCharacter(Transform t)
    {
        currentCharacter = t;

        if (t != null)
        {
            Color color = new Color(255f, 0f, 60f, 255f);
            t.Find("selectedBorder").GetComponent<Image>().color = color;
        }


        if (t != null)
        {
            int index = t.GetSiblingIndex();
            CharacterClass character = CharacterRooster.instance.characters[index];
            CharacterRooster.instance.ShowCharacterInSlot(0, character);
        }
        else
        {
            CharacterRooster.instance.ShowCharacterInSlot(0, null);

        }
        
    }
    void TokenFollow(bool trigger)
    {
        hasToken = trigger;
    }

}
