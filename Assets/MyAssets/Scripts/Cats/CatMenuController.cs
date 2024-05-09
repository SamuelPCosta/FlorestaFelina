using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CatMenuController : PanelController
{

    private GameObject option;

    private InventoryController inventoryController;
    private UICollect _UICollect;

    public int catWaterConsumption;

    // Start is called before the first frame update
    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
        _UICollect = FindObjectOfType<UICollect>();
        _UIButtons = FindAnyObjectByType<UIButtons>();

        option = null;
        menu.SetActive(false);
    }

    void Update()
    {
        if (menu.activeSelf && confirmOption.triggered)
            interactWithCat();

    }

    public void turnOn()
    {
        menu?.SetActive(true);
        _UIButtons.setButtons(buttons);
        option = null;
        EventSystem.current.SetSelectedGameObject(checkOption());
    }

    public void selectOption(GameObject optionButton)
    {
        option = optionButton;
    }

    private GameObject checkOption() //ativa ou desativa opcao baseado...
    {
        int water = inventoryController.getCollectible(CollectibleType.WATER);

        bool enableOption1 = water > 0;
        bool enableOption2 = true;
        bool enableOption3 = false;

        setOptions(options[0], enableOption1);
        setOptions(options[1], enableOption2);
        setOptions(options[2], enableOption3);

        _UIButtons.disableOptions();
        GameObject firstOption = null;
        //Ordem inversa para a reescrita sempre sobrescrever com um numero menor
        //TODO: transformar isso numa funcao na panelController
        if (enableOption3)
        {
            _UIButtons.enableOption((int)MenuOption.OPTION3);
            firstOption = options[2];
        }
        if (enableOption2)
        {
            _UIButtons.enableOption((int)MenuOption.OPTION2);
            firstOption = options[1];
        }
        if (enableOption1)
        {
            _UIButtons.enableOption((int)MenuOption.OPTION1);
            firstOption = options[0];
        }

        return firstOption;
    }

    public void interactWithCat()
    {
        if (option == null)
            return;
        if (option == options[0])
        {
            inventoryController.consumeCollectible(CollectibleType.WATER, catWaterConsumption);
        }
        else
        if (option == options[1])
        {

        }
        else
        if (option == options[2])
        {

        }

        _UICollect.refreshInventory(CollectibleType.WATER, inventoryController.getCollectible(CollectibleType.WATER));

        _UICollect.refreshInventory(PotionType.POTION1, inventoryController.getPotion(PotionType.POTION1));
        _UICollect.refreshInventory(PotionType.POTION2, inventoryController.getPotion(PotionType.POTION2));
        _UICollect.refreshInventory(PotionType.POTION3, inventoryController.getPotion(PotionType.POTION3));

        option = null;
        EventSystem.current.SetSelectedGameObject(checkOption());
    }
}
