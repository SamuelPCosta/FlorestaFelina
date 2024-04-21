using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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

    public void saveTutorial(int index)
    {
        save.tutorialsLvl1[index] = false;
        saveGame(save);
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
}
