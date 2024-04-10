using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasController : MonoBehaviour
{
    public enum cam { Default, Close, Workbench, Cat, Shop };

    [Header("Cameras")]
    public GameObject[] Cameras;

    public void ActivateCamera(int index)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            if (i == index)
            {
                if(Cameras[i] != null)
                    Cameras[i].SetActive(true);
            }
            else
            {
                if (Cameras[i] != null)
                    Cameras[i].SetActive(false);
            }
        }
    }

    //CinemachineVirtualCamera.m_Lens.FieldOfView = FOVClose;
}
