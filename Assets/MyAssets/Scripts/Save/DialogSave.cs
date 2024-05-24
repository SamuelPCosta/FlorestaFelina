using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSave : MonoBehaviour
{
    public GameObject[] dialogs;
    void Start()
    {
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null)
        {
            int LevelIndex = GameController.getLevelIndex();
            for (int i = 0; i < dialogs.Length; i++)
            {
                bool checkedDialog = save.dialogs[LevelIndex, i];
                if (!checkedDialog)
                    dialogs[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
        else
        {
            foreach (GameObject dialog in dialogs)
                dialog.GetComponent<BoxCollider>().enabled = true;
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
