using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasController : MonoBehaviour
{
    public enum cam { Default, Close, Workbench, Cat };

    [Header("Cameras")]
    public GameObject[] Cameras;

    public void ActivateCamera(int index)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            if (i == index)
            {
                Cameras[i].SetActive(true);
            }
            else
            {
                Cameras[i].SetActive(false);
            }
        }
    }

    //CinemachineVirtualCamera.m_Lens.FieldOfView = FOVClose;
}
