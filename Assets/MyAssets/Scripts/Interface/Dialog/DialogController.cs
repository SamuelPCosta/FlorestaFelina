using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogController : UIController
{
    [Header("Dialog attributes")]
    public GameObject dialogPanel;
    public GameObject dialogIndicator;
    public TextMeshProUGUI nameOfCharacter;
    public TextMeshProUGUI speechOfCharacter;
    public Image imageOfCharacter;

    [Header("Characters")]
    public string mainCharacter = "Eilidh";

    [Header("Photos")]
    public Sprite[] photos;

    public float timeBtwChars = 0.3f;

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
    }

    void Update()
    {
        if (dialog.triggered && gameObject.activeSelf)
        {
            if(speechOfCharacter.maxVisibleCharacters < speechOfCharacter.textInfo.characterCount)
            {
                StopAllCoroutines();
                speechOfCharacter.maxVisibleCharacters = speechOfCharacter.textInfo.characterCount;
            }
            else
            {
                bool onDialog = refreshDialog();
                if (!onDialog)
                {
                    FindObjectOfType<InteractionsController>().exitDialog();
                    turnOffDialog();
                }
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

    public void turnOffDialog()
    {
        dialogPanel.SetActive(false);
    }

    public bool refreshDialog()
    {
        if (speeches != null && index >= speeches.Length)
            return false;

        Speeches.Character character = speeches[index].character;

        string speech = speeches[index].speech;
        speechOfCharacter.text = speech;
        StartCoroutine(textEffect());

        string name = "";
        Speeches.Character photo = 0;
        if(character == Speeches.Character.PLAYER)
        {
            name = mainCharacter;
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

    private IEnumerator textEffect()
    {
        speechOfCharacter.ForceMeshUpdate();
        int totalVisibleCharacters = speechOfCharacter.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCounter = counter % (totalVisibleCharacters + 1);
            speechOfCharacter.maxVisibleCharacters = visibleCounter;

            if (visibleCounter >= totalVisibleCharacters)
                break;

            ++counter;
            yield return new WaitForSeconds(timeBtwChars);
        }
    }
}
