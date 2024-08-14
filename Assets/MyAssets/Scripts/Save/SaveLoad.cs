using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    //Save jogo
    private Save save;
    private string nameOfSave = "save0";

    [Header("Save options")]
    public bool _ignoreSave = false;
    public bool deleteSaveFile = false;

    [Header("Debug options")]
    public bool isDebugging = false;

    public static SaveLoad instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if (deleteSaveFile)
            DeleteSaveFile();

        save = new Save();
    }

    void DeleteSaveFile(){
        string path = Application.persistentDataPath; //AppData/LocalLow
        string filePath = Path.Combine(path, nameOfSave + ".save");

        if (File.Exists(filePath)){
            File.Delete(filePath);
            if(isDebugging)
                Debug.Log("Save file deleted.");
        }
        else
            Debug.LogWarning("Save file does not exist.");
    }

    private void saveGame(Save save)
    {
        if (_ignoreSave)
            return;

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath; //AppData/LocalLow
        FileStream file = File.Create(path + "/" + nameOfSave +".save");
        formatter.Serialize(file, save);
        file.Close();
    }

    public Save loadGame()
    {
        if (_ignoreSave)
            return null;

        string path = Application.persistentDataPath;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file;

        if (File.Exists(path + "/" + nameOfSave + ".save"))
        {
            file = File.Open(path + "/" + nameOfSave + ".save", FileMode.Open);
            Save load = (Save)formatter.Deserialize(file);
            file.Close();

            return load;
        }
        return null;
    }

    private Save loadGameInternal() {
        Save save = loadGame();
        return save ?? new Save();
    } 

    //void PrintAllDialogs()
    //{
    //    string table = "";

    //    for (int row = 0; row < loadGame().dialogs.GetLength(0); row++)
    //    {
    //        for (int col = 0; col < loadGame().dialogs.GetLength(1); col++)
    //        {
    //            bool value = save.dialogs[row, col];
    //            table += value ? "1 " : "0 ";
    //        }
    //        table += "\n";
    //    }

    //    Debug.Log(table);
    //}

    public void saveDialog(int index)
    {
        Save save = loadGameInternal();
        save.dialogs[GameController.getLevelIndex(), index] = true;
        saveGame(save);
    }

    public void saveInventoryCollectibles(int water, int plant1, int plant2)
    {
        Save save = loadGameInternal();
        save.water = water;
        save.plant1 = plant1;
        save.plant2 = plant2;

        //Debug.Log(save.water +" - "+ save.plant1 + " - " + save.plant2);
        saveGame(save);
    }

    public void saveInventoryPotions(int potion1, int potion2, int potion3)
    {
        Save save = loadGameInternal();
        save.potion1 = potion1;
        save.potion2 = potion2;
        save.potion3 = potion3;

        saveGame(save);
    }

    public void saveMissionState(int index, MISSION_STATE state){
        Save save = loadGameInternal();
        save.missionState[index] = state;
        saveGame(save);

        if (isDebugging)
            Debug.Log("estado salvo");
    }

    public void saveMission(int mission, int stage, bool savePosition)
    {
        Save save = loadGameInternal();
        save.currentMission = mission;
        save.currentMissionStage = stage;
        if (savePosition){
            Vector3 position = FindObjectOfType<GameController>().getPlayerPosition();
            save.playerPosition[0] = position.x;
            save.playerPosition[1] = position.y;
            save.playerPosition[2] = position.z;
        }

        saveGame(save);
    }

    public void resetMission()
    {
        Save save = loadGameInternal();
        save.currentMission = -1;
        save.currentMissionStage = -1;

        saveGame(save);
    }

    public void setJournal()
    {
        Save save = loadGameInternal();
        ++save.journal;

        saveGame(save);
    }

    //public void savePlayerPosition(Transform position) {
    //    Save save = loadGameInternal();
    //    save.playerPosition[0] = position.position.x;
    //    save.playerPosition[1] = position.position.y;
    //    save.playerPosition[2] = position.position.z;

    //    if(isDebugging)
    //        print(position.position.x + " - " + position.position.y + " - " + position.position.z);

    //    saveGame(save);
    //}

    public void savePlayerPositionPortal(Transform position, int orientation, int levelIndex){
        Save save = loadGameInternal();
        save.playerPositionPortal[0] = position.position.x;
        save.playerPositionPortal[1] = position.position.y;
        save.playerPositionPortal[2] = position.position.z;
        save.orientation = orientation;
        save.previousLevel = levelIndex;

        if(isDebugging)
            print("Level: " + levelIndex +" - "+ position.position.x +" - "+ position.position.y +" - "+ position.position.z);

        saveGame(save);
    }
    
    public void saveLevel(int lvl){
        Save save = loadGameInternal();

        save.level = lvl;
        //save.playerPosition[0] = position.x;
        //save.playerPosition[1] = position.y;
        //save.playerPosition[2] = position.z;

        saveGame(save);
    }

    public void checkCameraObjective()
    {
        Save save = loadGameInternal();
        save.objectiveFloorDestroy = true;
        saveGame(save);
    }

    public void setStepOne(){
        Save save = loadGameInternal();
        save.step = true;
        saveGame(save);
    }
}
