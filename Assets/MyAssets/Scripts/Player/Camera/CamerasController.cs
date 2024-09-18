using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasController : MonoBehaviour
{
    public enum cam { Default, Close, Roomba, Objective, ObjectiveDontReload };
    private int lowPriority = 5;
    private int highPriority = 10;

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
        camera.GetComponent<CinemachineVirtualCamera>().Priority = highPriority;
        camera.SetActive(true);
    }
    public void DeactivateDynamicCamera(GameObject camera1, GameObject camera2)
    {
        if (camera1 != null)
            camera1.GetComponent<CinemachineVirtualCamera>().Priority = lowPriority;
        if (camera2 != null)
            camera2.GetComponent<CinemachineVirtualCamera>().Priority = lowPriority;
    }

    //CinemachineVirtualCamera.m_Lens.FieldOfView = FOVClose;
}
