using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;

public class InputManagerController : MonoBehaviour
{
    public float horizontal;
    public void Move(InputAction.CallbackContext context)
    { 
        horizontal = context.ReadValue<Vector2>().x;
        print("test");
    }
}
