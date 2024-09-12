using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CustomNav : MonoBehaviour
{
    public bool gamepadOn = false;
    private Vector2 lastMousePosition;

    private enum Input { PC, GAMEPAD };

    void Update()
    {
        checkInput();
        if (gamepadOn)
            ToggleGamepad(Input.GAMEPAD);
        else
            ToggleGamepad(Input.PC);
    }

    private void checkInput()
    {
        bool mouseMove = false;
        Vector2 currentMousePosition = new Vector2(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y);
        if (currentMousePosition != lastMousePosition)
        {
            if (lastMousePosition != Vector2.zero)
                mouseMove = true;

            lastMousePosition = Mouse.current.position.ReadValue();
        }

        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed || mouseMove)
        {
            if (gamepadOn == true)
                gamepadOn = false;
        }

        if (Gamepad.current != null)
        {
            foreach (InputControl control in Gamepad.current.allControls)
            {
                if (control.IsPressed() && gamepadOn == false)
                    gamepadOn = true;
            }
        }
    }

    private void ToggleGamepad(Input input)
    {
        if (input == Input.GAMEPAD)
            if (Gamepad.current.name.Equals("DualShock4GamepadHID")){

            }
        
    }
}
