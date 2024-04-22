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
            for (int i = 0; i < dialogs.Length; i++)
            {
                string level = SceneManager.GetActiveScene().name;
                bool state;
                if (level == "Level1")
                    state = save.dialogsLvl1[i];
                else if (level == "Level2")
                    state = save.dialogsLvl2[i];
                else if (level == "Level3")
                    state = save.dialogsLvl3[i];
                else if (level == "Level4")
                    state = save.dialogsLvl4[i];
                else if (level == "Level5")
                    state = save.dialogsLvl5[i];
                else
                    state = true;

                if (!state)
                    dialogs[i].SetActive(false);
            }
        }
    }

    public void saveDialog(GameObject tdialogAtual)
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            if (tdialogAtual == dialogs[i])
            {
                FindFirstObjectByType<SaveLoad>().saveDialog(i);
                return;
            }
        }
    }
}
