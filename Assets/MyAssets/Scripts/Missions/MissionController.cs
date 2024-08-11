using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mission { TUTORIAL, THIRST, PAIN, INJURED, VERY_INJURED}

[System.Serializable] public enum MISSION_STATE { NOT_STARTED, FIRST_INTERACTION, STARTED, HEALED, HOME } 

public class MissionController : MonoBehaviour{

    [Header("Workbench and inventory")]
    [Tooltip("WorkbenchController")]
    public WorkbenchController craft;
    public InventoryController inventoryController;

    [Header("UI")]
    public UIMission _UIMission;

    [Header("Missions - declaration")]
    public MissionType[] missionType = new MissionType[5];
    
    private int CurrentMission;
    private int CurrentStageMission;

    //variaveis de craft
    private bool hasWater;
    private int water;
    private int plant1;
    private int plant2;
    private int potion1;
    private int potion2;
    private int potion3;

    private int oldWater;
    private int oldPotion1;
    private int oldPotion2;
    private int oldPotion3;

    public static MissionController instance = null;
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if(save != null){
            CurrentMission = save.currentMission;
            CurrentStageMission = save.currentMissionStage;
            if (CurrentMission > -1){
                _UIMission.setMission(missionType[(int)CurrentMission]);
                _UIMission.setMissionStage(CurrentStageMission);
            }
            else
                _UIMission.disableMissionHUD();
        }
        else{
            CurrentMission = -1;
            CurrentStageMission = 0;
            _UIMission.disableMissionHUD();
        }
    }

    public void setMission(Mission mission)
    {
        CurrentMission = (int)mission;
        setMissionStage();

        CurrentStageMission = CurrentStageMission < 0 ? 0 : CurrentStageMission;
        _UIMission.setMission(missionType[(int)mission]);
        _UIMission.setMissionStage(CurrentStageMission);
    }

    public Mission getMission()
    {
        return (Mission)CurrentMission;
    }

    public void setMissionStage(){
        Mission mission = (Mission)CurrentMission;

        //variaveis
        hasWater = inventoryController.getCollectible(CollectibleType.WATER) >= CatController.catWaterConsumption;
        water = inventoryController.getCollectible(CollectibleType.WATER);
        plant1 = inventoryController.getCollectible(CollectibleType.PLANT1);
        plant2 = inventoryController.getCollectible(CollectibleType.PLANT2);
        potion1 = inventoryController.getPotion(PotionType.POTION1);
        potion2 = inventoryController.getPotion(PotionType.POTION2);
        potion3 = inventoryController.getPotion(PotionType.POTION3);

        //Relativo ao estagio 1>2
        if (CurrentStageMission == 0 && checkIngredients(mission))
            //Avanca estagioS da missao atual (levando em conta saltos de etapas ao iniciar missao)
            addStage();

        if(CurrentStageMission == 1)
            checkAlternativeText(mission);

        if (CurrentStageMission == 1 && checkMedicated(mission)){
            addStage();
            if (CurrentMission >= 0 && CurrentMission < missionType.Length)
                FindObjectOfType<CatsStatesController>().setMissionState(MISSION_STATE.HEALED);
            checkTutorial(mission);
        }
            

        checkMissionCompletion();

        oldWater = water;
        oldPotion1 = potion1;
        oldPotion2 = potion2;
        oldPotion3 = potion3;

        FindObjectOfType<SaveLoad>().saveMission(CurrentMission, CurrentStageMission);
    }

    private bool checkIngredients(Mission mission){
        bool nextStage = false;
        switch (mission) {
            case Mission.TUTORIAL: case Mission.THIRST:
                nextStage = hasWater;
                break;
            case Mission.PAIN:
                nextStage = water >= craft.potion1Water && plant1 >= craft.potion1Item1;
                break;
            case Mission.INJURED:
                nextStage = water >= craft.potion2Water && plant2 >= craft.potion2Item1;
                break;
            case Mission.VERY_INJURED:
                nextStage = potion1 >= craft.potion3Item1 && potion2 >= craft.potion3Item2;
                break;
        }
        return nextStage;
    }

    private void checkAlternativeText(Mission mission){
        bool alternativeText = false;
        string potion = "";

        switch (mission) {
            case Mission.PAIN:
                alternativeText = potion1 >= 1;
                potion = Potion.getPotionName(PotionType.POTION1);
                break;
            case Mission.INJURED:
                alternativeText = potion2 >= 1;
                potion = Potion.getPotionName(PotionType.POTION2);
                break;
            case Mission.VERY_INJURED:
                alternativeText = potion3 >= 1;
                potion = Potion.getPotionName(PotionType.POTION3);
                break;
            default: break;
        }

        if(alternativeText)
            _UIMission.setAlternativeText(potion);
    }

    private bool checkMedicated(Mission mission){
        bool nextStage = false;
        switch (mission) {
            case Mission.TUTORIAL:
            case Mission.THIRST:
                nextStage = water < oldWater;
                break;
            case Mission.PAIN:
                nextStage = potion1 < oldPotion1;
                break;
            case Mission.INJURED:
                nextStage = potion2 < oldPotion2;
                break;
            case Mission.VERY_INJURED:
                nextStage = potion3 < oldPotion3;
                break;
            default: break;
        }
        return nextStage;
    }

    private void checkTutorial(Mission mission) {
        //condicao de gato curado
        //if (CurrentMission >= 0 && CurrentMission < missionType.Length && CurrentStageMission == missionType[CurrentMission].description.Length - 1)
        //    FindObjectOfType<CatsStatesController>().setMissionState(MISSION_STATE.HEALED);

        //condicao de ativar ultimo dialogo do tutorial
        if (mission == Mission.TUTORIAL && CurrentStageMission >= missionType[CurrentMission].description.Length - 1) {
            GameController gameController = FindObjectOfType<GameController>();
            gameController.enableDialog(gameController.catDialog2, true);
        }
    }

    public void checkMissionCompletion(){
        //condicao de conclusao das missoes
        int numberOfSteps = missionType[CurrentMission].description.Length;
        if (CurrentMission >= 0 && CurrentMission < missionType.Length && CurrentStageMission >= numberOfSteps){
            _UIMission.completeMission();

            Debug.Log("Missao concluida");

            //reseta missao no save
            FindObjectOfType<SaveLoad>().resetMission();
            return;
        }
        else{ //atualiza na HUD e salva
            _UIMission.setMissionStage(CurrentStageMission);
        }
    }

    public void addStage(){
        ++CurrentStageMission;
    }

    public int getMissionStage(){
        return CurrentStageMission;
    }
}
