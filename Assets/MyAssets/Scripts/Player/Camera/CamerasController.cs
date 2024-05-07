using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasController : MonoBehaviour
{
    public enum cam { Default, Close, Workbench, Objective, ObjectiveDontReload };

    [Header("Cameras")]
    public GameObject[] Cameras;

    public void ActivateCamera(cam camera)
    {
        int index = (int)camera;
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

    public void ActivateDynamicCamera(GameObject camera)
    {
        foreach (var cam in Cameras)
        {
            if (cam != null)
                cam.SetActive(false);
        }
        camera.SetActive(true);
    }

    //CinemachineVirtualCamera.m_Lens.FieldOfView = FOVClose;
}
