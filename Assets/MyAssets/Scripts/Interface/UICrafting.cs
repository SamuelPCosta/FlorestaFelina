using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICrafting : MonoBehaviour
{
    public TextMeshProUGUI colletText;
    public TextMeshProUGUI water;
    public TextMeshProUGUI plant1;
    public TextMeshProUGUI plant2;

    public TextMeshProUGUI potion1;
    public TextMeshProUGUI potion2;
    public TextMeshProUGUI potion3;

    [Header("Colors")]
    public Color color1;
    public Color color2;


    // PRIVATE
    private List<Coroutine> collectCoroutines = new List<Coroutine>();

    // Start is called before the first frame update
    void Start()
    {
        colletText.gameObject.SetActive(false);

        //TODO: get save
        water.text = "" + 0;
        plant1.text = "" + 0;
        plant2.text = "" + 0;

        potion1.text = "" + 0;
        potion2.text = "" + 0;
        potion3.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            TextMeshProUGUI btnText = button.GetComponentInChildren<TextMeshProUGUI>();
            btnText.color = color2;
        }
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        if(btnSelected != null)
        {
            TextMeshProUGUI btnSelectedText = btnSelected.GetComponentInChildren<TextMeshProUGUI>();
            btnSelectedText.color = color1;
        }
    }

    public void spawnCollectText(CollectibleType type, string itemName, int quantity, bool allowed)
    {   
        colletText.gameObject.SetActive(true);
        
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

        colletText.text = "Coletou <b>" + itemName + "</b>";
        if (type != CollectibleType.WATER)
            colletText.text += " (" + quantity + "x).";
        else
            colletText.text += ".";

        if (!allowed) //excecao da agua no maximo
            colletText.text = "Seu cantil já está cheio!";

        //Lista controla comportamento de animacoes em cima de outra
        foreach (Coroutine c in collectCoroutines)
            StopCoroutine(c);

        //Anima opacidade do texto
        Coroutine coroutine = StartCoroutine(animateOpacity(colletText.gameObject));
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

    private IEnumerator animateOpacity(GameObject text)
    {
        float duration = 2.5f;
        Animator textAnimator = text.GetComponent<Animator>();

        textAnimator.Play("FadeInText", 0, 0f);
        yield return new WaitForSeconds(duration);

        textAnimator.Play("FadeOutText");

        yield return new WaitForSeconds(.5f);
        colletText.gameObject.SetActive(false);
    }
}
