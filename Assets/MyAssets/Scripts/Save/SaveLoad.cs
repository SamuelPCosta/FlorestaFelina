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

    void Start()
    {
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

    public void saveDialog(int index)
    {
        string level = SceneManager.GetActiveScene().name;
        if (level == "Level1")
            save.dialogsLvl1[index] = false;
        else if (level == "Level2")
            save.dialogsLvl2[index] = false;
        else if (level == "Level3")
            save.dialogsLvl3[index] = false;
        else if (level == "Level4")
            save.dialogsLvl4[index] = false;
        else if (level == "Level5")
            save.dialogsLvl5[index] = false;

        saveGame(save);
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
    
}
