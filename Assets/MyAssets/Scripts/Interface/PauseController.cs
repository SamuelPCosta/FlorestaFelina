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
    private InputAction esc;

    public bool isPaused = false;
    private JournalController journalController = null;

    void Awake()
    {
        input = new Inputs();
    }

    void OnEnable()
    {
        pauseBtn = input.Player.Pause;
        esc = input.Player.Esc;
        pauseBtn.Enable();
        esc.Enable();
    }

    void OnDisable()
    {
        pauseBtn.Disable();
        esc.Disable();
    }

    void Start()
    {
        panel.SetActive(false);
    }

    InteractionsController interactionsController = null;
    private bool isOnMenu = false;
    private bool isJournalOpen = false;
    public void pause(bool state){
        if (state == panel.activeSelf)
            return;

        panel.SetActive(state);

        if (state){
            StopAllCoroutines();
            StartCoroutine(resetButton());
            mainOptions.SetActive(true);
            Time.timeScale = 0;
            configOption.SetActive(false);
            FindObjectOfType<MenusController>().pauseAudio(true);
            FindObjectOfType<UIButtons>().setButtons(FindObjectOfType<MenusController>().buttons);
            EventSystem.current.SetSelectedGameObject(option1);
        }
        else
        {
            Time.timeScale = 1;
            MenusController menusController = FindObjectOfType<MenusController>();
            menusController.SetCursorState(false);
            menusController.pauseAudio(false);
        }
        //interactionsController.enabled = state;
        isPaused = !isPaused;
    }

    private void refreshValues(){
        interactionsController ??= FindObjectOfType<InteractionsController>();
        journalController ??= FindObjectOfType<JournalController>();
        isOnMenu = interactionsController.isOnMenu;
        isJournalOpen = journalController.isOpen;
    }

    private bool exitMenu(){
        if (isOnMenu || isJournalOpen){
            return true;
        }
        return false;
    }

    private IEnumerator resetButton()
    {
        panel.transform.GetChild(2).gameObject.SetActive(true);
        panel.transform.GetChild(3).gameObject.SetActive(false);
        yield return null;
        FindObjectOfType<MenusController>().selectOption(option1);
    }

    public void Unpause()
    {
        pause(false);
    }

    void LateUpdate(){
        refreshValues();
        if (esc.triggered){
            if(!exitMenu())
                pause(!panel.activeSelf);
        }
        else
        if (pauseBtn.triggered){
            pause(!panel.activeSelf);
        }
    }
}
