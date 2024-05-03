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
    private string nameOfSave;

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
        nameOfSave = "save0";
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
        Save loadedSave = loadGame();
        if (loadedSave == null)
            loadedSave = new Save();
        loadedSave.dialogs[GameController.getLevelIndex(), index] = true;
        saveGame(loadedSave);
    }

    public void saveInventoryCollectibles(int water, int plant1, int plant2)
    {
        save.water = water;
        save.plant1 = plant1;
        save.plant2 = plant2;

        print(save.plant1);

        saveGame(save);
    }

    public void saveInventoryPotions(int potion1, int potion2, int potion3)
    {
        save.potion1 = potion1;
        save.potion2 = potion2;
        save.potion3 = potion3;

        saveGame(save);
    }

    public void savePlayerPosition(Transform player)
    {
        save.playerX = player.position.x;
        save.playerY = player.position.y;
        save.playerZ = player.position.z;
    }
    
    public void saveLevel()
    {
        save.level = SceneManager.GetActiveScene().buildIndex;
    }
}
