using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogController : MonoBehaviour
{
    [Header("Dialog attributes")]
    public GameObject dialogPanel;
    public GameObject dialogIndicator;
    public TextMeshProUGUI nameOfCharacter;
    public TextMeshProUGUI speechOfCharacter;
    public Image imageOfCharacter;

    [Header("Photos")]
    public Sprite[] photos;

    private Speeches.Speech[] speeches;
    private Inputs input;
    private InputAction dialog;

    private int index;

    private void Awake()
    {
        input = new Inputs();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        dialog = input.Player.Dialog;
        speeches = null;
        index  = 0;
        dialogPanel.SetActive(false);
        spawnNPCTextIndicator(false);
    }

    void Update()
    {
        if (gameObject.activeSelf && dialog.triggered)
        {
            //TODO: passar falas
            bool onDialog = refreshDialog();
            if (!onDialog)
            {
                FindObjectOfType<InteractionsController>().exitDialog();
                dialogPanel.SetActive(false);
            }
        }
    }

    public void setSpeeches(Speeches.Speech[] speeches)
    {
        this.speeches = speeches;
    }

    public void turnOnDialog()
    {
        dialogPanel.SetActive(true);
        index = 0;
        refreshDialog();
    }

    public bool refreshDialog()
    {
        if (speeches != null && index >= speeches.Length)
            return false;

        Speeches.Character character = speeches[index].character;
        string speech = speeches[index].speech;

        speechOfCharacter.text = speech;

        string name = "";
        Speeches.Character photo = 0;
        if(character == Speeches.Character.PLAYER)
        {
            name = "Protagonista";
            photo = Speeches.Character.PLAYER;
        }
        else
        if (character == Speeches.Character.NPC1)
        {
            name = "NPC1";
            photo = Speeches.Character.NPC1;
        }
        //TODO terminar com base na enum

        nameOfCharacter.text = name;
        imageOfCharacter.sprite = photos[(int)photo];

        index++;

        return true;
    }

    public void spawnNPCTextIndicator(bool state)
    {
        if (state)
            dialogIndicator.SetActive(true);
        else
            dialogIndicator.SetActive(false);
    }
}
