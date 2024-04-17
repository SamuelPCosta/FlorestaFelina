using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICollect : UIController
{
    [Header("Collect texts")]
    public TextMeshProUGUI collectedItemText;
    public GameObject collectText;

    [Header("Collectibles texts")]
    public TextMeshProUGUI water;
    public TextMeshProUGUI plant1;
    public TextMeshProUGUI plant2;

    [Header("Potion texts")]
    public TextMeshProUGUI potion1;
    public TextMeshProUGUI potion2;
    public TextMeshProUGUI potion3;

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
}
