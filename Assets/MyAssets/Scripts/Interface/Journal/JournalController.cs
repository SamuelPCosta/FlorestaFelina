using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JournalController : MonoBehaviour
{
    [Header("Objects")]
    public GameObject JournalMenu;
    public GameObject[] pages;

    //PRIVATES
    private Inputs input;
    private int page = 0;
    private int pageLimit;

    //ACTIONS
    private InputAction journal;
    private InputAction prev;
    private InputAction next;

    private void Awake(){
        input = new Inputs();
    }

    private void OnEnable(){
        input.Enable();
    }

    private void OnDisable(){
        input.Disable();
    }

    void Start()
    {
        JournalMenu?.SetActive(false);
        pageLimit = FindObjectOfType<SaveLoad>().loadGame().journal;

        journal = input.Player.Journal;
        prev = input.Player.Prev;
        next = input.Player.Next;
    }

    void Update()
    {
        checkJournal();
    }

    private void checkJournal() {
        bool state = JournalMenu.activeSelf;
        if (journal.triggered){
            JournalMenu.SetActive(!state);
            if (!state) { //abrir
                viewPage(0);
                Time.timeScale = 0f;
            } else
                Time.timeScale = 1f;
            return;
        }

        if (!state)
            return;

        if (prev.triggered)
            viewPage(Mathf.Max(0, --page));
        if (next.triggered)
            viewPage(Mathf.Min(++page, pages.Length-1));
    }

    private void viewPage(int num){
        if (num > pageLimit)
            return;
        pages[num].SetActive(true);
        for(int i = 0; i < pages.Length; i++)
            if(i != num)
                pages[i].SetActive(false);
    }

    public void addPage()
    {
        FindObjectOfType<SaveLoad>().setJournal();
        ++pageLimit;
    }
}
