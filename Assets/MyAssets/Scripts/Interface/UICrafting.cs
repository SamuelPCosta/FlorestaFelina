using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICrafting : MonoBehaviour
{
    [Header("Collect texts")]
    public TextMeshProUGUI collectedItemText;
    public GameObject collectText;
    public TextMeshProUGUI water;
    public TextMeshProUGUI plant1;
    public TextMeshProUGUI plant2;

    [Header("Potion texts")]
    public TextMeshProUGUI potion1;
    public TextMeshProUGUI potion2;
    public TextMeshProUGUI potion3;

    [Header("Buttons - potionOptions")]
    public GameObject option1;
    public GameObject option2;
    public GameObject option3;

    [Header("Values  - potion1")]
    public GameObject option1item1;
    public GameObject option1item2;

    public GameObject option2item1;
    public GameObject option2item2;

    public GameObject option3item1;
    public GameObject option3item2;

    [Header("Indicator  - potionOption")]
    public GameObject indicator1;
    public GameObject indicator2;
    public GameObject indicator3;

    [Header("Colors")]
    public Color color1;
    public Color color2;

    private GameObject btnCurrent;

    public enum PotionsOption { OPTION1, OPTION2, OPTION3};
    private enum InventoryItem { PLANT1, PLANT2, WATER, POTION1, POTION2 }

    // PRIVATE
    private List<Coroutine> collectCoroutines = new List<Coroutine>();

    // Start is called before the first frame update
    void Start()
    {
        collectedItemText.gameObject.SetActive(false);

        //TODO: get save
        water.text = "" + 0;
        plant1.text = "" + 0;
        plant2.text = "" + 0;

        potion1.text = "" + 0;
        potion2.text = "" + 0;
        potion3.text = "" + 0;

        btnCurrent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Button[] buttons = FindObjectsOfType<Button>();
        //foreach (Button button in buttons)
        //{
        //    TextMeshProUGUI btnText = button.GetComponentInChildren<TextMeshProUGUI>();
        //    btnText.color = color2;
        //}
        //GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        //if(btnSelected != null)
        //{
        //    TextMeshProUGUI btnSelectedText = btnSelected.GetComponentInChildren<TextMeshProUGUI>();
        //    btnSelectedText.color = color1;
        //}

        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        if (btnSelected == null && btnCurrent != null)
        {
            btnSelected = btnCurrent;
            EventSystem.current.SetSelectedGameObject(btnSelected);
        }

        if (btnSelected != null)
            selectOption(btnSelected);

        print(EventSystem.current.currentSelectedGameObject);
    }

    public void disableOptions()
    {
        TextMeshProUGUI[] texts;
        texts = option1.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
            text.color = color2;

        texts = option2.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
            text.color = color2;

        texts = option3.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
            text.color = color2;

        indicator1.SetActive(false);
        indicator2.SetActive(false);
        indicator3.SetActive(false);
    }

    public void enableOption(PotionsOption optionPotion)
    {
        GameObject option = null;

        if (PotionsOption.OPTION1 == optionPotion)
            option = option1;
        else
        if (PotionsOption.OPTION2 == optionPotion)
            option = option2;
        else
        if (PotionsOption.OPTION3 == optionPotion)
            option = option3;

        TextMeshProUGUI[] texts = option.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
            text.color = color1;
    }

    private void selectOption(GameObject btnSelected)
    {
        indicator1.SetActive(false);
        indicator2.SetActive(false);
        indicator3.SetActive(false);

        GameObject indicator = null;

        if (option1 == btnSelected)
            indicator = indicator1;
        else
        if (option2 == btnSelected)
            indicator = indicator2;
        else
        if (option3 == btnSelected)
            indicator = indicator3;

        btnCurrent = indicator.transform.parent.gameObject;
        indicator.SetActive(true);
    }

    public void spawnCollectText(bool state)
    {
        if (state)
            collectText.gameObject.SetActive(true);
        else
            collectText.gameObject.SetActive(false);
    }

    public void spawnCollectedItemText(CollectibleType type, string itemName, int quantity, bool allowed)
    {   
        collectedItemText.gameObject.SetActive(true);
        
        //Collectible collectible = null;
        //switch (type)
        //{
        //    case CollectibleType.PLANT1:
        //        collectible = GameObject.FindObjectOfType<PlantA>();
        //        break;
        //    case CollectibleType.PLANT2:
        //        collectible = GameObject.FindObjectOfType<PlantA>(); //TODO CORRIGIR NOME DA CLASSE
        //        break;
        //    case CollectibleType.WATER:
        //        collectible = GameObject.FindObjectOfType<Water>();
        //        break;
        //}

        //string itemName = collectible.getNameOfItem();

        collectedItemText.text = "Coletou <b>" + itemName + "</b>";
        if (type != CollectibleType.WATER)
            collectedItemText.text += " (" + quantity + "x).";
        else
            collectedItemText.text += ".";

        if (!allowed) //excecao da agua no maximo
            collectedItemText.text = "Seu cantil já está cheio!";

        //Lista controla comportamento de animacoes em cima de outra
        foreach (Coroutine c in collectCoroutines)
            StopCoroutine(c);

        //Anima opacidade do texto
        Coroutine coroutine = StartCoroutine(animateOpacity(collectedItemText.gameObject));
        collectCoroutines.Add(coroutine);
    }

    public void refreshInventory(CollectibleType type, int quantity)
    {
        switch (type)
        {
            case CollectibleType.PLANT1: 
                plant1.text = "" + quantity;
                break;
            case CollectibleType.PLANT2:
                plant2.text = "" + quantity;
                break;
            case CollectibleType.WATER:
                water.text = "" + quantity;
                break;
        }
    }

    public void refreshInventory(PotionType type, int quantity)
    {
        switch (type)
        {
            case PotionType.POTION1:
                potion1.text = "" + quantity;
                break;
            case PotionType.POTION2:
                potion2.text = "" + quantity;
                break;
            case PotionType.POTION3:
                potion3.text = "" + quantity;
                break;
        }
    }

    public void refreshWorkbench(int[] inventory, int[] craft1, int[] craft2, int[] craft3)
    {
        option1item1.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.PLANT1] + "/" + craft1[0];
        option1item2.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.WATER] + "/" + craft1[1];

        option2item1.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.PLANT2] + "/" + craft2[0];
        option2item2.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.WATER] + "/" + craft2[1];

        option3item1.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.POTION1] + "/" + craft3[0];
        option3item2.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.POTION2] + "/" + craft3[1];
    }

    private IEnumerator animateOpacity(GameObject text)
    {
        float duration = 2.5f;
        Animator textAnimator = text.GetComponent<Animator>();

        textAnimator.Play("FadeInText", 0, 0f);
        yield return new WaitForSeconds(duration);

        textAnimator.Play("FadeOutText");

        yield return new WaitForSeconds(.5f);
        collectedItemText.gameObject.SetActive(false);
    }
}
