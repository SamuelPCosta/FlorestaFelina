using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogSave : MonoBehaviour
{
    public GameObject[] dialogs;
    void Start()
    {
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null)
        {
            string level = SceneManager.GetActiveScene().name;
            bool[] levelDialogs;

            switch (level)
            {
                case "Level1":
                    levelDialogs = save.dialogsLvl1;
                    break;
                case "Level2":
                    levelDialogs = save.dialogsLvl2;
                    break;
                case "Level3":
                    levelDialogs = save.dialogsLvl3;
                    break;
                case "Level4":
                    levelDialogs = save.dialogsLvl4;
                    break;
                case "Level5":
                    levelDialogs = save.dialogsLvl5;
                    break;
                default:
                    levelDialogs = null;
                    break;
            }

            for (int i = 0; i < dialogs.Length; i++)
            {
                bool state = true;
                state = levelDialogs[i];

                if (!state)
                    dialogs[i].SetActive(false);
            }
        }
    }

    public void saveDialog(GameObject currentDialog)
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            if (currentDialog == dialogs[i])
            {
                FindObjectOfType<SaveLoad>().saveDialog(i);
                return;
            }
        }
    }
}
