using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WorkbenchController : MonoBehaviour
{
    [Header("Crafting Menu")]
    public GameObject craftingMenu;
    public GameObject option1;
    public GameObject option2;
    public GameObject option3;

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

    private GameObject potion;

    //ACTIONS
    private Inputs input;
    private InputAction craft;

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
        craft = input.Player.Craft;
        inventoryController = FindObjectOfType<InventoryController>();
        _UICrafting = FindObjectOfType<UICrafting>();

        potion = null;
        craftingMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (craftingMenu.activeSelf && craft.triggered)
            craftPotion();

        GodMode();
    }

    //PRIVATES 
    private void GodMode()
    {
        if (input.Player.extraW.triggered)
        {
            inventoryController.addCollectible(CollectibleType.WATER, 2);
            _UICrafting.refreshInventory(CollectibleType.WATER, inventoryController.getCollectible(CollectibleType.WATER));
        }
        if (input.Player.extraP1.triggered)
        {
            inventoryController.addCollectible(CollectibleType.PLANT1, 2);
            _UICrafting.refreshInventory(CollectibleType.PLANT1, inventoryController.getCollectible(CollectibleType.PLANT1));
        }
        if (input.Player.extraP2.triggered)
        {
            inventoryController.addCollectible(CollectibleType.PLANT2, 2);
            _UICrafting.refreshInventory(CollectibleType.PLANT2, inventoryController.getCollectible(CollectibleType.PLANT2));
        }
    }

    //PUBLICS
    public void turnOnMenu()
    {
        craftingMenu.SetActive(true);
        potion = null;
        EventSystem.current.SetSelectedGameObject(checkOption());
    }

    public void turnOffMenu()
    {
        craftingMenu.SetActive(false);
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

        setOptions(option1, enableOption1);
        setOptions(option2, enableOption2);
        setOptions(option3, enableOption3);

        _UICrafting.disableOptions();
        GameObject firstOption = null;
        //Ordem inversa para a reescrita sempre sobrescrever com um numero menor
        if (enableOption3)
        {
            _UICrafting.enableOption(UICrafting.PotionsOption.OPTION3);
            firstOption = option3;
        }
        if (enableOption2)
        {
            _UICrafting.enableOption(UICrafting.PotionsOption.OPTION2);
            firstOption = option2;
        }
        if (enableOption1)
        {
            _UICrafting.enableOption(UICrafting.PotionsOption.OPTION1);
            firstOption = option1;
        }

        return firstOption;
    }

    private void setOptions(GameObject button, bool condition)
    {
        if (condition)
            button.GetComponent<Button>().interactable = true;
        else
            button.GetComponent<Button>().interactable = false;
    }

    public void craftPotion()
    {
        if(potion == null)
            return;
        if (potion == option1)
        {
            inventoryController.consumeCollectible(CollectibleType.PLANT1, potion1Item1);
            inventoryController.consumeCollectible(CollectibleType.WATER, potion1Water);

            inventoryController.addPotion(PotionType.POTION1);
        }
        else
        if (potion == option2)
        {
            inventoryController.consumeCollectible(CollectibleType.PLANT2, potion2Item1);
            inventoryController.consumeCollectible(CollectibleType.WATER, potion2Water);

            inventoryController.addPotion(PotionType.POTION2);
        }
        else
        if (potion == option3)
        {
            inventoryController.consumePotion(PotionType.POTION1, potion3Item1);
            inventoryController.consumePotion(PotionType.POTION2, potion3Item2);

            inventoryController.addPotion(PotionType.POTION3);
        }

        _UICrafting.refreshInventory(CollectibleType.PLANT1, inventoryController.getCollectible(CollectibleType.PLANT1));
        _UICrafting.refreshInventory(CollectibleType.PLANT2, inventoryController.getCollectible(CollectibleType.PLANT2));
        _UICrafting.refreshInventory(CollectibleType.WATER, inventoryController.getCollectible(CollectibleType.WATER));

        _UICrafting.refreshInventory(PotionType.POTION1, inventoryController.getPotion(PotionType.POTION1));
        _UICrafting.refreshInventory(PotionType.POTION2, inventoryController.getPotion(PotionType.POTION2));
        _UICrafting.refreshInventory(PotionType.POTION3, inventoryController.getPotion(PotionType.POTION3));

        potion = null;
        EventSystem.current.SetSelectedGameObject(checkOption());
    }
}
