using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mission { TUTORIAL, THIRST, MISSION3, MISSION4, MISSION5, MISSION6, MISSION7, MISSION8 } // TODO: ajustar numero do missionType[]

[System.Serializable] public enum MISSION_STATE { NOT_STARTED, FIRST_INTERACTION, STARTED, FINISH, HOME } 

public class MissionController : MonoBehaviour
{
    private int CurrentMission;
    private int CurrentStageMission;

    [Header("Workbench and inventory")]
    public WorkbenchController workbenchController;
    public InventoryController inventoryController;

    [Header("UI")]
    public UIMission _UIMission;

    [Header("Missions - declaration")]
    public MissionType[] missionType = new MissionType[8];

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

    //TODO: PROGRAMAR AS MISSOES DOS GATOS AQUI
    public void setMissionStage()
    {
        bool nextStage = false;
        Mission mission = (Mission)CurrentMission;

        switch (mission) {
            case Mission.TUTORIAL: case Mission.THIRST:
                bool hasWater = inventoryController.getCollectible(CollectibleType.WATER) >= CatController.catWaterConsumption;
                if (hasWater || CurrentStageMission > 0)
                    nextStage = true;
                break;

            case Mission.MISSION3:
                int water = inventoryController.getCollectible(CollectibleType.WATER);
                int plant1 = inventoryController.getCollectible(CollectibleType.PLANT1);
                if (water >= workbenchController.potion1Water && plant1 >= workbenchController.potion1Item1)
                    nextStage = true;
                break;
        }

        if (nextStage)
            CurrentStageMission++;

        //adotar gato
        if (CurrentMission >= 0 && CurrentMission < missionType.Length && CurrentStageMission == missionType[CurrentMission].description.Length - 1)
            FindObjectOfType<CatsStatesController>().setMissionState(MISSION_STATE.FINISH);

        //condicao de conclusao da missao
        if (CurrentMission >= 0 && CurrentMission < missionType.Length 
            && CurrentStageMission >= missionType[CurrentMission].description.Length){
            _UIMission.completeMission();
            if (mission == Mission.TUTORIAL){
                GameController gameController = FindObjectOfType<GameController>();
                gameController.enableDialog(gameController.catDialog2, true);
            }

            //reseta missao no save
            Debug.Log("Missao concluida");
            FindObjectOfType<SaveLoad>().resetMission();
            return;
        }
        else //atualiza na HUD e salva
        {
            _UIMission.setMissionStage(CurrentStageMission);
        }
        
        FindObjectOfType<SaveLoad>().saveMission(CurrentMission, CurrentStageMission);
    }

    public int getMissionStage()
    {
        return CurrentStageMission;
    }
}
