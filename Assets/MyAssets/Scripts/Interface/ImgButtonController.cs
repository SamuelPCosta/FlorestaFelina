using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.InputSystem;
using TMPro;


public class ImgButtonController : UIController
{
    private Vector2 lastMousePosition;
    public bool gamepadOn = false;
    private Gamepad gamepad;

    private enum Input {PC, GAMEPAD};

    [Header("Buttons")]
    public Image[] imgs;

    [Header("Imgs PC")]
    public Sprite[] sprites_pc;

    [Header("Imgs Gamepad")]
    public Sprite[] sprites_gamepad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        if (gamepadOn){
            print("Controle");
            ToggleIcons(Input.GAMEPAD);
        }
        else
            ToggleIcons(Input.PC);
    }

    private void checkInput()
    {
        bool mouseMove = false;
        Vector2 currentMousePosition = new Vector2(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y);
        if (currentMousePosition != lastMousePosition){
            if (lastMousePosition != Vector2.zero)
                mouseMove = true;
            lastMousePosition = Mouse.current.position.ReadValue();
        }

        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed || mouseMove){
            if (gamepadOn == true)
                gamepadOn = false;
        }

        if (Gamepad.current != null){
            foreach (InputControl control in Gamepad.current.allControls){
                if (control.IsPressed() && gamepadOn == false)
                    gamepadOn = true;
            }
        }
    }

    private void ToggleIcons(Input input){
        Sprite[] icons;
        if (input == Input.GAMEPAD)
            icons = sprites_gamepad;
        else
            icons = sprites_pc;

        int index = 0;
        foreach (Image img in imgs){
            if (img != null && img.gameObject.activeSelf)
                img.sprite = icons[index];
            index++;
        }
    }
}
