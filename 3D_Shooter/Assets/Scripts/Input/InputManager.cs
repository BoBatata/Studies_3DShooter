using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputControls inputControls;

    public Vector2 MoveDir => inputControls.Move.Walk.ReadValue<Vector2>();

    public InputManager(){
        inputControls = new InputControls();
        EnablePlayerInput();
    }


    public void EnablePlayerInput() => inputControls.Move.Enable();
    public void DisablePlayerInput() => inputControls.Move.Disable();
}
