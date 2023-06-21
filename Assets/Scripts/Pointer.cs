using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    private GraphicRaycaster graphicRay;
    private PointerEventData pointerEventData = new PointerEventData(null);
    // Start is called before the first frame update
    void Start()
    {
        graphicRay = GetComponentInParent<GraphicRaycaster>();

    }

    // Update is called once per frame
    void Update()
    {
        pointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRay.Raycast(pointerEventData, results);

        if (results.Count > 0 )
        {
            print(results[0].gameObject.name);
        }

    }
}
