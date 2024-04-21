using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSave : MonoBehaviour
{
    public GameObject[] tutorial;
    void Start()
    {
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null)
        {
            for (int i = 0; i < tutorial.Length; i++)
            {
                if (!save.tutorialsLvl1[i])
                    tutorial[i].SetActive(false);
            }
        }
    }

    public void saveTutorial(GameObject tutorialAtual)
    {
        for (int i = 0; i < tutorial.Length; i++)
        {
            if (tutorialAtual == tutorial[i])
            {
                FindFirstObjectByType<SaveLoad>().saveTutorial(i);
                return;
            }
        }
    }
}
