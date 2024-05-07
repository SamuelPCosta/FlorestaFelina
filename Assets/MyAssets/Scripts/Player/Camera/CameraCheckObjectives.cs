using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCheckObjectives : MonoBehaviour
{
    public GameObject floor;

    public void visited()
    {
        floor.SetActive(false);
        FindObjectOfType<SaveLoad>().checkCameraObjective();
    }   
}
