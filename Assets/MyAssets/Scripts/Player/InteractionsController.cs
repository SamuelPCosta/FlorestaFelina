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

        private UICollect _UICollect;

        //ACTIONS
        private InputAction workbench;
        private InputAction collet;
        private InputAction makeWay;


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
            _UICollect = FindObjectOfType<UICollect>();

            workbench = input.Player.Workbench;
            collet = input.Player.Collet;
            makeWay = input.Player.MakeWay;

            InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
            inputsCursor.SetCursorState(true);
        }

        // Update is called once per frame
        void Update()
        {
            checkCollectibles();
            checkWay();
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
                //Debug.Log("Coletar!");
                _UICollect.spawnCollectText(true);
                if (collet.triggered && collectible != null)
                    {
                    int quantityCollected = collectible.getQuantityOfItems();
                    CollectibleType type = collectible.getType();
                    bool isAdded = inventoryController.addCollectible(type, quantityCollected);

                    _UICollect.spawnCollectedItemText(type, collectible.getNameOfItem(), quantityCollected, isAdded);
                    _UICollect.refreshInventory(type, inventoryController.getCollectible(type));

                    collectible.collectItem();
                }
            }else
                _UICollect.spawnCollectText(false);
        }

        //CONTROLA A INTERACAO COM BARREIRAS
        private void checkWay()
        {
            Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Barrier"));
            if (collider != null)
            {
                print("Barreira "+ collider.gameObject.name);
                //_UICrafting.spawnCollectText(true); TODO UI - way
                if (makeWay.triggered) { }
                    //Chamar animacao --- e a animacao chama o metodo abaixo
                    //barrier.makeWay(); TODO
            }else { }
                //_UICrafting.spawnCollectText(true); TODO UI - way
        }

        //CONTROLA INTERACAO COM A BANCADA
        private void checkWorkbench()
        {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Workbench"));
        if (collider != null)
        {
            Debug.Log("Workbench a frente");
            if (workbench.triggered)
            {
                WorkbenchController workbenchController = GameObject.FindObjectOfType<WorkbenchController>();
                InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
                if (_workebenchCam)
                {
                    _workebenchCam = false;
                    workbenchController.turnOffMenu();
                    inputsCursor.SetCursorState(true);
                    transform.GetComponent<MovementController>().enablePlayerMovement(true);
                }
                else
                {
                    _workebenchCam = true;
                    workbenchController.turnOnMenu();
                    inputsCursor.SetCursorState(false);
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
