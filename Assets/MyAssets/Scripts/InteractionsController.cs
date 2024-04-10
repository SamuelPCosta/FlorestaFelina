using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputs
{
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class InteractionsController : MonoBehaviour
    {    
        
        [Header("Player object")]
        public GameObject PlayerRoot;
        public BoxCastController boxcast;

        [Header("Workbench")]
        public Transform workbenchOrigin;

        [Header("Cameras")]
        [Tooltip("CamerasController script")]
        public CamerasController camerasController;


        //PRIVATES
        private Inputs input;

        private bool _workebenchCam = false;
        private bool _ShopCam = false;

        private InputAction workbench;
        //private InputAction <>;

        private void Awake()
        {
            input = new Inputs();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        void Start()
        {
           workbench = input.Player.Workbench;
           //<> = input.Player.<>;
        }

        // Update is called once per frame
        void Update()
        {
            checkCollectibles();
            checkWorkbench();
            checkCameras();
        }

        private void checkCollectibles()
        {
            if (boxcast.checkBoxCast(LayerMask.NameToLayer("Collectible")))
                print("Coletavel");
        }

        private void checkWorkbench()
        {
            if (boxcast.checkBoxCast(LayerMask.NameToLayer("Workbench")))
            {
                print("Workbench a frente");
                if (workbench.triggered)
                {
                    print("Interagindo com a workbench");
                    if (_workebenchCam)
                    {
                        _workebenchCam = false;
                        transform.GetComponent<MovementController>().enablePlayerMovement(true);
                    }
                    else
                    {
                        _workebenchCam = true;
                        transform.GetComponent<MovementController>().enablePlayerMovement(false);
                        transform.GetComponent<MovementController>().moveTo(workbenchOrigin); //TODO CORRIGIR COMPORTAMENTO
                    }
                }
            }
        }

        private void checkCameras()
        {
            //Controla trasicao de cameras
            RaycastHit hit;
            if (Physics.Raycast(PlayerRoot.transform.position, Vector3.down, out hit))
            {
                if (!hit.collider.CompareTag("InnerRoom")) //AREA EXTERNA
                    camerasController.ActivateCamera((int)CamerasController.cam.Default);
                else //AREA INTERNA
                {
                    if(_workebenchCam)
                        camerasController.ActivateCamera((int)CamerasController.cam.Workbench);
                    else if(_ShopCam)
                        camerasController.ActivateCamera((int)CamerasController.cam.Workbench);
                    else
                        camerasController.ActivateCamera((int)CamerasController.cam.Close);
                }
                    
            }
        }
    }
}