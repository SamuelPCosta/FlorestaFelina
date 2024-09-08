using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    public GameObject option1;
    public GameObject panel;
    public GameObject mainOptions;
    public GameObject configOption;

    private Inputs input;
    private InputAction pauseBtn;

    public bool isPaused = false;

    void Awake()
    {
        input = new Inputs();
    }

    void OnEnable()
    {
        pauseBtn = input.Player.Pause;
        pauseBtn.Enable();
    }

    void OnDisable()
    {
        pauseBtn.Disable();
    }

    void Start()
    {
        panel.SetActive(false);
    }

    public void pause(bool state)
    {
        isPaused = state;
        panel.SetActive(state);
        if (state){
            FindObjectOfType<MenusController>().selectOption(option1);
            mainOptions.SetActive(true);
            configOption.SetActive(false);
            FindObjectOfType<MenusController>().pauseAudio(true);
            FindObjectOfType<UIButtons>().setButtons(FindObjectOfType<MenusController>().buttons);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(option1);
        }
        else
        {
            Time.timeScale = 1;
            FindObjectOfType<MenusController>().pauseAudio(false);
        }
        FindObjectOfType<InteractionsController>().enabled = !state;
    }

    public void Unpause()
    {
        pause(false);
    }

    void Update()
    {
        if (pauseBtn.triggered)
        {
            pause(!panel.activeSelf);
        }
    }
}
