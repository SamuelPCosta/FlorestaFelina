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

    [Header("Dialog")]
    public DialogController dialogController;

    [Header("Cat")]
    public CatMenuController catMenuController;

    [Header("Cameras")]
    [Tooltip("CamerasController script")]
    public CamerasController camerasController;


    //PRIVATES
    private Inputs input;

    private bool _workebenchCam = false;
    private bool _ShopCam = false;
    private bool _catInteraction = false;
    private GameObject catCamera;

    private bool inDialog = false;
    private bool inTutorial = false;
    private string nameOfTutorial = "";

    private UICollect _UICollect;

    //ACTIONS
    private InputAction workbench;
    private InputAction collet;
    private InputAction makeWay;
    private InputAction dialog;
    private InputAction cat;
    private InputAction moveFast;


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
        dialog = input.Player.Dialog;
        cat = input.Player.Dialog;
        moveFast = input.Player.MoveFast;

        catCamera = null;

        InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
        inputsCursor.SetCursorState(true);
    }

    // Update is called once per frame
    void Update()
    {
        checkCollectibles();
        checkWay();
        checkNPC();
        checkTutorial();
        checkCat();
        checkWorkbench();
        checkCameras();

        if (moveFast.triggered)
            transform.GetComponent<MovementController>().changeLocomotion();
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
            BarrierController barrier = collider.GetComponent<BarrierController>();
            //_UICrafting.spawnCollectText(true); TODO UI - way
            if (makeWay.triggered)
                //Chamar animacao --- e a animacao chama o metodo abaixo
                barrier.destroy();
        }else { }
            //_UICrafting.spawnCollectText(true); TODO UI - way
    }

    //CONTROLA A INTERACAO COM GATOS
    private void checkCat()
    {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Cat"));
        if (collider != null)
        {
            print("Gato");
            //TODO textos indicadores
            CatController catController = collider.GetComponent<CatController>();
                
            if (cat.triggered){

                InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
                if (!_catInteraction)
                {
                    catMenuController.turnOn();
                    transform.GetComponent<MovementController>().enablePlayerMovement(false);
                    catCamera = getCatCamera(collider.gameObject);
                    inputsCursor.SetCursorState(false);
                    _catInteraction = true;
                }
                else { 
                    catMenuController.turnOff();
                    transform.GetComponent<MovementController>().enablePlayerMovement(true);
                    inputsCursor.SetCursorState(true);
                    _catInteraction = false;
                }
            }
                    
        }else { }
            //_UICrafting.spawnCollectText(true); TODO UI - way
    }

    //CONTROLA A INTERACAO COM NPCs
    private void checkNPC()
    {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("NPC"));
        if (collider != null)
        {
            if(!inDialog)
                dialogController.spawnNPCTextIndicator(true);
            else
                dialogController.spawnNPCTextIndicator(false);

            if (dialog.triggered && !inDialog) {
                Speeches.Speech[]  speechs = collider.GetComponent<Speeches>().getSpeeches();
                dialogController.setSpeeches(speechs);
                dialogController.turnOnDialog();
                inDialog = true;
                transform.GetComponent<MovementController>().enablePlayerMovement(false);
            }
        }else
            dialogController.spawnNPCTextIndicator(false);
    }

    //CONTROLA A INTERACAO COM TUTORIAIS AUTOMATICOS
    private void checkTutorial()
    {
        string disableOnMove = "TutorialMovimentacao";
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Tutorial"));
        if (collider != null)
        {
            inTutorial = true;
            Speeches tutorial = collider.GetComponent<Speeches>();
            Speeches.Speech[] speechs = tutorial.getSpeeches();
            dialogController.setSpeeches(speechs);
            dialogController.turnOnDialog();

            nameOfTutorial = collider.gameObject.name;
            if (nameOfTutorial != disableOnMove)
                transform.GetComponent<MovementController>().enablePlayerMovement(false);

            tutorial.markTutorial();
        }
        else
        {
            if (inTutorial)
            {
                //Execao do tutorial de movimento
                Vector2 playerMovement = GetComponent<InputsMovement>().move;
                if (nameOfTutorial.Equals(disableOnMove) & playerMovement != Vector2.zero)
                {
                    dialogController.turnOffDialog();
                    inTutorial = false;
                }
            }
        }
    }

    public void exitDialog()
    {
        inDialog = false;
        inTutorial = false;
        transform.GetComponent<MovementController>().enablePlayerMovement(true);
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
            {
                if(!_catInteraction)
                    camerasController.ActivateCamera(CamerasController.cam.Default);
                else
                    camerasController.ActivateDynamicCamera(catCamera);
            }
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


    private GameObject getCatCamera(GameObject cat)
    {
        return cat.transform.GetChild(0).gameObject;
    }
}
