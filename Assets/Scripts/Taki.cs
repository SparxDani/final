using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Taki : CharacterBase
{
    public override void Update()
    {
        base.Update();
        horizontal = Input.GetAxis("Horizontal");
        
    }
    
}
