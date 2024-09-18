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

    public bool isOpen = false;

    //PRIVATES
    private Inputs input;
    private int page = 0;
    private int pageLimit = 0;

    //ACTIONS
    private InputAction journal;
    private InputAction prev;
    private InputAction next;
    private InputAction esc;

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
                bool active = (int)newPages[i].GetComponent<PagesController>().pageSequence < 4;
                newPages[i].SetActive(active);
            }
        }

        journal = input.Player.Journal;
        prev = input.Player.Prev;
        next = input.Player.Next;
        esc = input.Player.Esc;
    }

    void Update()
    {
        checkJournal();
    }

    private void checkJournal() {
        bool state = !JournalMenu.activeSelf;
        if (journal != null && journal.triggered){
            openJournal(state);
            return;
        }

        if (esc.triggered){
            openJournal(false);
            return;
        }

        if (state)
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

    private void openJournal(bool state){
        JournalMenu.SetActive(state);
        StopAllCoroutines();
        StartCoroutine(SetIsOpenDelayed(state));
        if (state)
        { //abrir
            viewPage(pageLimit);
            page = pageLimit;
            Time.timeScale = 0f;
        }
        else
            Time.timeScale = 1f;
        FindObjectOfType<InteractionsController>().setInteractions();
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

    private IEnumerator SetIsOpenDelayed(bool state)
    {
        yield return null;
        yield return null;

        isOpen = JournalMenu.activeSelf;
    }
}
