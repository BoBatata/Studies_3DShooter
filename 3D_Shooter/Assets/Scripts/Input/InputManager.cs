using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputControls inputControls;

    public Vector2 MoveDir => inputControls.Move.Walk.ReadValue<Vector2>();

    public InputManager(){
        inputControls = new InputControls();
        EnableInput();
    }


    public void EnableInput() => inputControls.Enable();
    public void DisableInput() => inputControls.Disable();
}
