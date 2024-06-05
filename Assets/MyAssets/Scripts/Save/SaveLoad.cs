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

    public bool _ignoreSave = false;
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

        save = new Save();
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
        Debug.Log("Salvo");
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

    public void saveMission(int mission, int stage)
    {
        Save save = loadGameInternal();
        save.currentMission = mission;
        save.currentMissionStage = stage;

        saveGame(save);
    }

    public void savePlayerPosition(Transform player)
    {
        Save save = loadGameInternal();
        save.playerX = player.position.x;
        save.playerY = player.position.y;
        save.playerZ = player.position.z;
    }
    
    public void saveLevel()
    {
        Save save = loadGameInternal();
        save.level = SceneManager.GetActiveScene().buildIndex;
    }

    public void checkCameraObjective()
    {
        Save save = loadGameInternal();
        save.objectiveFloorDestroy = true;
        saveGame(save);
    }
}
