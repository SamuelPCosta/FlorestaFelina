using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] buttons;
    protected GameObject[] options;

    [Header("Menu")]
    public GameObject menu;

    protected UIButtons _UIButtons;

    //ACTIONS
    protected Inputs input;
    protected InputAction confirmOption;

    public enum MenuOption { OPTION1, OPTION2, OPTION3, OPTION4, OPTION5 };

    protected void Awake()
    {
        input = new Inputs();
        confirmOption = input.Player.ConfirmOption;

        options = new GameObject[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
            options[i] = buttons[i].gameObject;
    }

    protected void OnEnable()
    {
        input.Enable();
    }

    protected void OnDisable()
    {
        input.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected void setOptions(GameObject button, bool condition)
    {
        if (condition)
            button.GetComponent<Button>().interactable = true;
        else
            button.GetComponent<Button>().interactable = false;
    }

    public void turnOff()
    {
        menu?.SetActive(false);
    }
}
