using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


    public class InteractionsController : MonoBehaviour
    {    
        
        [Header("Player object")]
        public GameObject PlayerRoot;
        public ProximityController boxcast;

        [Header("Inventory")]
        public InventoryController inventoryController;

        [Header("Workbench")]
        public Transform workbenchOrigin;

        [Header("Cameras")]
        [Tooltip("CamerasController script")]
        public CamerasController camerasController;


        //PRIVATES
        private Inputs input;

        private bool _workebenchCam = false;
        private bool _ShopCam = false;


        //ACTIONS
        private InputAction workbench;
        private InputAction collet;


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
           collet = input.Player.Collet;
        }

        // Update is called once per frame
        void Update()
        {
            checkCollectibles();
            checkWorkbench();
            checkCameras();
        }

        //CONTROLA COLETA E CONEXAO COM INVENTARIO
        private void checkCollectibles()
        {
            Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Collectible"));
            if (collider != null)
            {
                Collectible collectible = collider.GetComponent<Collectible>();
                Debug.Log("Coletar!");
                if (collet.triggered && collectible != null)
                {

                    int quantityCollected = collectible.getQuantityOfItems();
                    Debug.Log("Coletou - "+ quantityCollected + " "+ collectible.getNameOfItem());
                    inventoryController.addColletible(collectible.getType(), quantityCollected);
                    collectible.collectItem();
                }
            }  
        }

        //CONTROLA INTERACAO COM A BANCADA E MENUS
        private void checkWorkbench()
        {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Workbench"));
        if (collider != null)
        {
            Debug.Log("Workbench a frente");
            if (workbench.triggered)
            {
                WorkbenchController workbenchController = GameObject.FindObjectOfType<WorkbenchController>();
                if (_workebenchCam)
                {
                    _workebenchCam = false;
                    workbenchController.turnOffMenu();
                    transform.GetComponent<MovementController>().enablePlayerMovement(true);
                }
                else
                {
                    _workebenchCam = true;
                    workbenchController.turnOnMenu();
                    transform.GetComponent<MovementController>().enablePlayerMovement(false);
                    transform.GetComponent<MovementController>().moveTo(workbenchOrigin);
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
                    camerasController.ActivateCamera(CamerasController.cam.Default);
                else //AREA INTERNA
                {
                    if(_workebenchCam)
                        camerasController.ActivateCamera(CamerasController.cam.Workbench);
                    else if(_ShopCam)
                        camerasController.ActivateCamera(CamerasController.cam.Workbench);
                    else
                        camerasController.ActivateCamera(CamerasController.cam.Close);
                }
                    
            }
        }
    }
