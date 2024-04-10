using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    [Header("Player object")]
    public GameObject PlayerRoot;
    public BoxCastController boxcast;

    [Header("Cameras")]
    [Tooltip("CamerasController script")]
    public CamerasController camerasController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkCamera();

        if (boxcast.checkBoxCast(LayerMask.NameToLayer("Collectible")))
            print("Coletavel");

        if (boxcast.checkBoxCast(LayerMask.NameToLayer("Workbench")))
        {
            print("Workbench");
            //camerasController.ActivateCamera((int)CamerasController.cam.Workbench);
        }
            
    }

    private void checkCamera()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerRoot.transform.position, Vector3.down, out hit))
        {
            //Controla trasicao de cameras da areaInterna
            if (hit.collider.CompareTag("InnerRoom"))
                camerasController.ActivateCamera((int)CamerasController.cam.Close);
            else
                camerasController.ActivateCamera((int)CamerasController.cam.Default);
        }
    }
}
