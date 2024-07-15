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


    //public void Vibrate(float duration)
    //{
    //    if (gamepad != null)
    //    {
    //        // Definir a vibração nos motores esquerdo e direito
    //        gamepad = Gamepad.current;
    //        gamepad.SetMotorSpeeds(50, 50);
    //        GamePad.SetVibration(0, 50, 50);
    //        Invoke("StopVibration", duration);
    //    }
    //}

    //public void VibrateEspecial(float duration)
    //{
    //    if (gamepad != null)
    //    {
    //        // Definir a vibração nos motores esquerdo e direito
    //        gamepad = Gamepad.current;
    //        gamepad.SetMotorSpeeds(100, 100);
    //        GamePad.SetVibration(0, 100, 100);
    //        Invoke("StopVibration", duration);
    //    }
    //}

    //private void StopVibration()
    //{
    //    if (gamepad != null)
    //    {
    //        // Parar a vibração definindo a intensidade dos motores para zero
    //        gamepad.SetMotorSpeeds(0, 0);
    //        GamePad.SetVibration(0, 0, 0);
    //    }
    //}

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
