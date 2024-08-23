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
    public GameObject[] newPages;

    //PRIVATES
    private Inputs input;
    private int page = 0;
    private int pageLimit = 0;

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

    void Start(){
        JournalMenu?.SetActive(false);
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null)
            pageLimit = save.journal;
        else{
            for (int i = 0; i < newPages.Length; i++){
                bool active = (int)newPages[i].GetComponent<PagesController>().pageSequence < save.journal;
                newPages[i].SetActive(active);
            }
        }

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
                viewPage(pageLimit);
                page = pageLimit;
                Time.timeScale = 0f;
            } else
                Time.timeScale = 1f;
            FindObjectOfType<InteractionsController>().setInteractions();
            return;
        }

        if (!state)
            return;

        if (prev.triggered){
            page = Mathf.Max(--page, 0);
            viewPage(page);
        }
        if (next.triggered) {
            page = Mathf.Min(++page, pageLimit);
            viewPage(page);
        }
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
