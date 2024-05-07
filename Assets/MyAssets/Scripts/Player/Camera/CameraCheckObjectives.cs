using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCheckObjectives : MonoBehaviour
{
    public GameObject floor;

    public void visited(int index)
    {
        floor.SetActive(false);
        FindObjectOfType<SaveLoad>().checkCameraObjective(index);
    }   
}
