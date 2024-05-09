using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCheckObjectives : MonoBehaviour
{
    public GameObject floor;

    public void disable()
    {
        floor.SetActive(false);
    }

    public void visited()
    {
        disable();
        FindObjectOfType<SaveLoad>().checkCameraObjective();
    }   
}
