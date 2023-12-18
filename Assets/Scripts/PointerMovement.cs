using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerMovement : MonoBehaviour
{
    public float speed;

    private Vector2 movementInput;

    private void FixedUpdate()
    {
        UpdateMovement();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        
    }
    private void UpdateMovement()
    {
        float x = movementInput.x;
        float y = movementInput.y;

        transform.position += new Vector3(x, y, 0) * Time.deltaTime * speed;

        Vector3 worldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -worldSize.x, worldSize.x), Mathf.Clamp(transform.position.y, -worldSize.y, worldSize.y), transform.position.z);
    }

}
