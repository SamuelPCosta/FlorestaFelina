using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDontReload : MonoBehaviour
{
    public GameObject floor;
    void Start()
    {
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null)
        {
            if(save.objectiveFloorDestroy)
                floor?.SetActive(false);
        }
    }
}
