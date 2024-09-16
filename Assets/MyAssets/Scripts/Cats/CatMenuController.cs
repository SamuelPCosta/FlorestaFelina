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

    private bool isThirsty = true;
    private CatController catController;

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

    public void turnOn(){
        menu?.SetActive(true);
        _UIButtons.setButtons(buttons);
        option = null;
        isThirsty = true;
        EventSystem.current.SetSelectedGameObject(checkOption());
    }

    public void selectOption(GameObject optionButton){
        option = optionButton;
    }

    private GameObject checkOption() //ativa ou desativa opcao baseado na missao
    {
        MissionController missionController = FindObjectOfType<MissionController>();
        Mission mission = missionController.getMission();
        int stageMission = missionController.getMissionStage();

        int water = inventoryController.getCollectible(CollectibleType.WATER);
        bool hasfish = inventoryController.getCollectible(CollectibleType.FISH) > 0;

        bool hasWater = false;
        bool hasPotion = false;
        switch (mission){
            case Mission.TUTORIAL: case Mission.THIRST:
                if (water > 0)
                    hasWater = true;
                break;
            case Mission.PAIN:
                if (inventoryController.getPotion(PotionType.POTION1) > 0)
                    hasPotion = true;
                break;
            case Mission.INJURED:
                if (inventoryController.getPotion(PotionType.POTION2) > 0)
                    hasPotion = true;
                break;
            case Mission.VERY_INJURED:
                if (inventoryController.getPotion(PotionType.POTION3) > 0)
                    hasPotion = true;
                break;
        }

        bool enableOption1 = hasWater && isThirsty;
        bool enableOption2 = hasfish;
        bool enableOption3 = hasPotion;

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

    public void interactWithCat()
    {
        MissionController missionController = FindObjectOfType<MissionController>();
        if (missionController == null)
            return;
        Mission mission = missionController.getMission();
        int stageMission = missionController.getMissionStage();

        if (option == null)
            return;
        if (option == options[0]){
            inventoryController.consumeCollectible(CollectibleType.WATER, 2);
            isThirsty = false;

            if(stageMission == 1)
                missionController.setMissionStage();
            else if (stageMission >= 0)
                missionController.setOldsIngredients();
            AudioController.playAction(INTERACTIONS.Drinking);
        }
        else
        if (option == options[1]){
            //TODO: cena de alimentacao
            inventoryController.consumeCollectible(CollectibleType.FISH, 1);
            AudioController.playAction(INTERACTIONS.Eating);
        }
        else
        if (option == options[2]){
            switch (mission){
                case Mission.PAIN:
                    inventoryController.consumePotion(PotionType.POTION1, 1);
                    break;
                case Mission.INJURED:
                    inventoryController.consumePotion(PotionType.POTION2, 1);
                    break;
                case Mission.VERY_INJURED:
                    inventoryController.consumePotion(PotionType.POTION3, 1);
                    break;
            }
            if (stageMission == 1)
                missionController.setMissionStage();
            else if(stageMission >= 0)
                missionController.setOldsIngredients();

            AudioController.playAction(INTERACTIONS.Drinking);
            //TODO: cena de remedio
        }

        _UICollect.refreshInventory(CollectibleType.WATER, inventoryController.getCollectible(CollectibleType.WATER));
        _UICollect.refreshInventory(CollectibleType.FISH, inventoryController.getCollectible(CollectibleType.FISH));

        _UICollect.refreshInventory(PotionType.POTION1, inventoryController.getPotion(PotionType.POTION1));
        _UICollect.refreshInventory(PotionType.POTION2, inventoryController.getPotion(PotionType.POTION2));
        _UICollect.refreshInventory(PotionType.POTION3, inventoryController.getPotion(PotionType.POTION3));

        option = null;
        EventSystem.current.SetSelectedGameObject(checkOption());
    }

    public void exit()
    {
        option = null;
    }
}
