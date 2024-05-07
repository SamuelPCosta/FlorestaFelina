using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorActiveController : MonoBehaviour
{
    public GameObject[] floors;
    void Start()
    {
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null)
        {
            if (save.objective1Level1)
                floors[0]?.SetActive(false);

            if (save.objective2Level1)
                floors[1]?.SetActive(false);
        }
        else
        {
            foreach (GameObject floor in floors)
                floor.SetActive(true);
        }
    }
}
