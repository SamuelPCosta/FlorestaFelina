using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WorkbenchController : PanelController
{
    [Header("Workbench values - Potion 1")]
    public int potion1Item1;
    public int potion1Water;

    [Header("Workbench values - Potion 2")]
    public int potion2Item1;
    public int potion2Water;

    [Header("Workbench values - Potion 3")]
    public int potion3Item1;
    public int potion3Item2;

    private InventoryController inventoryController;
    private UICrafting _UICrafting;
    private UICollect _UICollect;

    private GameObject potion;

    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
        _UICrafting = FindObjectOfType<UICrafting>();
        _UICollect = FindObjectOfType<UICollect>();
        _UIButtons = FindAnyObjectByType<UIButtons>();

        potion = null;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.activeSelf && confirmOption.triggered)
            craftPotion();

        GodMode();
    }

    //PRIVATES 
    private void GodMode()
    {
        if (input.Godmode.extraW.triggered)
        {
            inventoryController.addCollectible(CollectibleType.WATER, 2);
            _UICollect.refreshInventory(CollectibleType.WATER, inventoryController.getCollectible(CollectibleType.WATER));
        }
        if (input.Godmode.extraP1.triggered)
        {
            inventoryController.addCollectible(CollectibleType.PLANT1, 2);
            _UICollect.refreshInventory(CollectibleType.PLANT1, inventoryController.getCollectible(CollectibleType.PLANT1));
        }
        if (input.Godmode.extraP2.triggered)
        {
            inventoryController.addCollectible(CollectibleType.PLANT2, 2);
            _UICollect.refreshInventory(CollectibleType.PLANT2, inventoryController.getCollectible(CollectibleType.PLANT2));
        }
    }

    //PUBLICS
    public void turnOnMenu()
    {
        menu?.SetActive(true);
        _UIButtons.setButtons(buttons);
        potion = null;
        EventSystem.current.SetSelectedGameObject(checkOption());
    }

    public void selectPotion(GameObject potionButton)
    {
        potion = potionButton;
    }

    private GameObject checkOption() //ativa ou desativa opcao baseado na quantidade de itens do inventario
    {
        int plant1 = inventoryController.getCollectible(CollectibleType.PLANT1);
        int plant2 = inventoryController.getCollectible(CollectibleType.PLANT2);
        int water = inventoryController.getCollectible(CollectibleType.WATER);
        int potion1 = inventoryController.getPotion(PotionType.POTION1);
        int potion2 = inventoryController.getPotion(PotionType.POTION2);

        bool enableOption1 = (plant1 >= potion1Item1 && water >= potion1Water);
        bool enableOption2 = (plant2 >= potion2Item1 && water >= potion2Water);
        bool enableOption3 = (potion1 >= potion3Item1 && potion2 >= potion3Item2);

        int[] inventory = {plant1, plant2, water, potion1, potion2};
        int[] craft1 = {potion1Item1, potion1Water};
        int[] craft2 = {potion2Item1, potion2Water};
        int[] craft3 = {potion3Item1, potion3Item2};

        _UICrafting.refreshWorkbench(inventory, craft1, craft2, craft3);

        _UIButtons.setOptions(options[0], enableOption1);
        _UIButtons.setOptions(options[1], enableOption2);
        _UIButtons.setOptions(options[2], enableOption3);

        _UIButtons.disableOptions();
        GameObject firstOption = null;
        //Ordem inversa para a reescrita sempre sobrescrever com um numero menor
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

    public void craftPotion()
    {
        if(potion == null)
            return;
        if (potion == options[0])
        {
            inventoryController.consumeCollectible(CollectibleType.PLANT1, potion1Item1);
            inventoryController.consumeCollectible(CollectibleType.WATER, potion1Water);

            inventoryController.addPotion(PotionType.POTION1);
        }
        else
        if (potion == options[1])
        {
            inventoryController.consumeCollectible(CollectibleType.PLANT2, potion2Item1);
            inventoryController.consumeCollectible(CollectibleType.WATER, potion2Water);

            inventoryController.addPotion(PotionType.POTION2);
        }
        else
        if (potion == options[2])
        {
            inventoryController.consumePotion(PotionType.POTION1, potion3Item1);
            inventoryController.consumePotion(PotionType.POTION2, potion3Item2);

            inventoryController.addPotion(PotionType.POTION3);
        }
        AudioController.playAction(INTERACTIONS.Potion);

        _UICollect.refreshInventory(CollectibleType.PLANT1, inventoryController.getCollectible(CollectibleType.PLANT1));
        _UICollect.refreshInventory(CollectibleType.PLANT2, inventoryController.getCollectible(CollectibleType.PLANT2));
        _UICollect.refreshInventory(CollectibleType.WATER, inventoryController.getCollectible(CollectibleType.WATER));

        _UICollect.refreshInventory(PotionType.POTION1, inventoryController.getPotion(PotionType.POTION1));
        _UICollect.refreshInventory(PotionType.POTION2, inventoryController.getPotion(PotionType.POTION2));
        _UICollect.refreshInventory(PotionType.POTION3, inventoryController.getPotion(PotionType.POTION3));

        potion = null;
        EventSystem.current.SetSelectedGameObject(checkOption());
    }
}
