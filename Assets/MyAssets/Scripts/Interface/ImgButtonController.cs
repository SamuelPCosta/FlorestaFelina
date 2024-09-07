using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class ImgButtonController : UIController{
    private Vector2 lastMousePosition;
    public bool gamepadOn = false;

    private enum Input {PC, GAMEPAD};

    [Header("Buttons")]
    public Image[] imgs;

    [Header("Imgs PC")]
    public Sprite[] sprites_pc;

    [Header("Imgs Gamepad")]
    public Sprite[] sprites_gamepad;

    void Update(){
        checkInput();
        if (gamepadOn)
            ToggleIcons(Input.GAMEPAD);
        else
            ToggleIcons(Input.PC);
    }

    private void checkInput(){
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
            if (img != null && img.gameObject.activeSelf) {
                img.sprite = icons[index];
                img.rectTransform.sizeDelta = setAspectoRatio(icons[index], img);
            }
            index++;
        }
    }

    private Vector2 setAspectoRatio(Sprite sprite, Image img){
        float originalWidth = sprite.texture.width;
        float originalHeight = sprite.texture.height;
        float height = img.rectTransform.rect.height;
        float width = (originalWidth / originalHeight) * height;
        return new Vector2(width, height);
    }
}
