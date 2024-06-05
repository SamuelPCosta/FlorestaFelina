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

    [Header("WorkbenchOriginPoint")]
    public Transform workbenchOrigin;

    [Header("Dialog")]
    public DialogController dialogController;

    [Header("CatMenu")]
    public CatMenuController catMenuController;

    [Header("Cameras")]
    [Tooltip("CamerasController script")]
    public CamerasController camerasController;

    [Header("MagicMakeWay")]
    public MagicController magic;

    [Header("UITextIndicator")]
    public UITextIndicator _UITextIndicator;


    //PRIVATES
    private Inputs input;

    private bool _workebenchCam = false;
    private bool _ShopCam = false;
    private bool _catInteraction = false;
    private GameObject catCamera;

    private bool enableMovement = true;

    private bool inDialog = false;
    private bool inDynamicDialog = false;
    private bool executeActionByDialog = true;
    private string nameOfTutorial = "";
    private bool unlock = false;

    private UICollect _UICollect;

    private bool fastMovementAllowed = false;

    //ACTIONS
    private InputAction menu;
    private InputAction collect;
    private InputAction makeWay;
    private InputAction dialog;
    private InputAction moveFast;
    private InputAction nextLevel;
    private InputAction showAffection;

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

        menu = input.Player.Menu;
        collect = input.Player.Collect;
        makeWay = input.Player.MakeWay;
        dialog = input.Player.Dialog;
        moveFast = input.Player.MoveFast;
        nextLevel = input.Player.NextLevel;
        showAffection = input.Player.ShowAffection;

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
        checkDialogs();
        checkCat();
        checkWorkbench();
        checkGate();
        checkCameras();

        if (moveFast.triggered && fastMovementAllowed)
            transform.GetComponent<MovementController>().changeLocomotion();
    }

    //CONTROLA COLETA E CONEXAO COM INVENTARIO
    private void checkCollectibles()
    {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Collectible"));
        if (collider != null)
        {
            Collectible collectible = collider.GetComponent<Collectible>();
            _UITextIndicator.enableIndicator(IndicatorText.COLLECT, true);
            if (collect.triggered && collectible != null)
                {
                int quantityCollected = collectible.getQuantityOfItems();
                CollectibleType type = collectible.getType();
                bool isAdded = inventoryController.addCollectible(type, quantityCollected);

                _UICollect.spawnCollectedItemText(type, collectible.getNameOfItem(), quantityCollected, isAdded);
                _UICollect.refreshInventory(type, inventoryController.getCollectible(type));

                collectible.collectItem();
            }
        }else
            _UITextIndicator.enableIndicator(IndicatorText.COLLECT, false);
    }

    //CONTROLA A INTERACAO COM BARREIRAS
    private void checkWay()
    {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Barrier"));
        if (collider != null)
        {
            if(!unlock)
                _UITextIndicator.enableIndicator(IndicatorText.BARRIER, true);
            else
                _UITextIndicator.enableIndicator(IndicatorText.BARRIER, false);
            //GameObject barrier = collider.gameObject;
            if (makeWay.triggered && !unlock){
                collider.GetComponent<BarrierController>().makeWay();
                //magic.castMagic(barrier);
                unlock = true;
            }
        }
        else{
            _UITextIndicator.enableIndicator(IndicatorText.BARRIER, false);
            unlock = false;
        }
            
    }

    //CONTROLA A INTERACAO COM GATOS
    private void checkCat()
    {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Cat"));
        if (collider != null){
            fastMovementAllowed = false;

            CatController catController = collider.GetComponent<CatController>();
            bool analyzed = catController.getAnalyzedStts();

            if (!_catInteraction)
                _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, true);

            if (analyzed)
            {
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
                if(!_catInteraction)
                    _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, true);
            }
            else
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, true);

            if (showAffection.triggered && !_catInteraction) //impede carinho quando o menu esta aberto
            {
                print("carinho");
                //TODO: ativar animacao
            }

            if (!analyzed && menu.triggered)
            {
                catController.analyzeCat();
            } 

            if (analyzed && menu.triggered){
                InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
                if (_catInteraction) {
                    _catInteraction = false;
                    catMenuController.turnOff();
                    enableMovement = true;
                    inputsCursor.SetCursorState(true);
                }
                else {
                    _catInteraction = true;
                    catMenuController.turnOn();
                    enableMovement = false;
                    catCamera = getCatCamera(collider.gameObject);
                    inputsCursor.SetCursorState(false);
                    _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, false);
                    _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, false);
                    _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
                }
            }    
        }else
        {
            fastMovementAllowed = true;
            _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, false);
            _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, false);
            _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
        }
    }

    //CONTROLA A INTERACAO COM NPCs
    private void checkNPC()
    {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("NPC"));
        if (collider != null)
        {
            if(!inDialog)
                _UITextIndicator.enableIndicator(IndicatorText.NPC, true);
            else
                _UITextIndicator.enableIndicator(IndicatorText.NPC, false);

            if (dialog.triggered && !inDialog) {
                Speeches.Speech[] speechs = collider.GetComponent<Speeches>().getSpeeches();
                dialogController.setSpeeches(speechs);
                dialogController.turnOnDialog();
                inDialog = true;
                enableMovement = false;
            }

            fastMovementAllowed = false;
        }
        else
        {
            _UITextIndicator.enableIndicator(IndicatorText.NPC, false);
            fastMovementAllowed = true;
        }
    }

    //CONTROLA A INTERACAO COM DIALOGOS AUTOMATICOS
    private void checkDialogs()
    {
        Speeches.Speech[] speechs;
        
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Tutorial"));
        if (collider != null)
        {
            if (inDynamicDialog)
                return;

            //inicia dialogo
            inDynamicDialog = true;
            Speeches dialog = collider.GetComponent<Speeches>();
            speechs = dialog.getSpeeches();
            dialogController.setSpeeches(speechs);
            dialogController.turnOnDialog();
            nameOfTutorial = collider.gameObject.name;
            enableMovement = false;

            //excecao do tutorial de movimento
            //if (nameOfTutorial != disableOnMove)
            //    enableMovement = false;

            //gatilho pra acoes
            if (executeActionByDialog)
                enableElements(nameOfTutorial);

            //gerenciar salvamento e reload
            dialog.markDialog();

            fastMovementAllowed = false;
        }
        else
        {
            if (inDynamicDialog)
                inDynamicDialog = false;

            //Execao do tutorial de movimento
            //Vector2 playerMovement = GetComponent<InputsMovement>().move;
            //string disableOnMove = "MoveTutorial";
            //if (nameOfTutorial.Equals(disableOnMove) && playerMovement != Vector2.zero)
            //    dialogController.turnOffDialog();

            fastMovementAllowed = true;
            executeActionByDialog = true;
        }
    }

    public void exitDialog()
    {
        inDialog = false;
        //inDynamicDialog = false;
        enableMovement = true;
    }

    //CONTROLA INTERACAO COM A BANCADA
    private void checkWorkbench()
    {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Workbench"));
        if (collider != null)
        {
            if(!_workebenchCam)
                _UITextIndicator.enableIndicator(IndicatorText.WORKBENCH, true);
            if (menu.triggered)
            {
                WorkbenchController workbenchController = GameObject.FindObjectOfType<WorkbenchController>();
                InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
                if (_workebenchCam)
                {
                    _workebenchCam = false;
                    workbenchController.turnOff();
                    inputsCursor.SetCursorState(true);
                    enableMovement = true;
                }
                else
                {
                    _workebenchCam = true;
                    workbenchController.turnOnMenu();
                    inputsCursor.SetCursorState(false);
                    enableMovement = false;
                    transform.GetComponent<MovementController>().moveTo(workbenchOrigin);

                    _UITextIndicator.enableIndicator(IndicatorText.WORKBENCH, false);
                }
            }
        }else
            _UITextIndicator.enableIndicator(IndicatorText.WORKBENCH, false);
    }

    //CONTROLA INTERACAO COM PORTOES
    public void checkGate()
    {
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Gate"));
        if (collider != null)
        {
            _UITextIndicator.enableIndicator(IndicatorText.GATE, true);
            if (nextLevel.triggered)
            {
                FindObjectOfType<GameController>().nextScene();
            }
        }
        else
            _UITextIndicator.enableIndicator(IndicatorText.GATE, false);
    }

    private void checkCameras()
    {
        //Controla trasicao de cameras
        RaycastHit hit;
        if (Physics.Raycast(PlayerRoot.transform.position, Vector3.down, out hit))
        {
            if (!hit.collider.CompareTag("InnerRoom")) //AREA EXTERNA
            {
                if (!_catInteraction)
                {
                    if (hit.collider.CompareTag("EnvironmentView"))
                        camerasController.ActivateCamera(CamerasController.cam.Objective);
                    else if (hit.collider.CompareTag("EnvironmentViewDontReload"))
                        camerasController.ActivateCamera(CamerasController.cam.ObjectiveDontReload);
                    else
                        camerasController.ActivateCamera(CamerasController.cam.Default);
                }
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

            //MOVIMENTACAO BLOQUEADA
            if (hit.collider.CompareTag("EnvironmentView")
                        || hit.collider.CompareTag("EnvironmentViewDontReload") || !enableMovement)
                transform.GetComponent<MovementController>().enablePlayerMovement(false);
            else
                transform.GetComponent<MovementController>().enablePlayerMovement(true);

        }
    }

    private GameObject getCatCamera(GameObject cat)
    {
        return cat.transform.GetChild(0).gameObject;
    }

    private void enableElements(string name)
    {
        GameController gameController = FindFirstObjectByType<GameController>();

        if (name.Equals("MoveTutorial"))
        {
            gameController.enableDialog(gameController.catDialog, false);
            gameController.enableTutorialCat(false);
        }

        if (name.Equals("WorkbenchTutorial"))
        {
            gameController.enableTutorialCat(true);
            gameController.enableDialog(gameController.catDialog, true);
        }

        executeActionByDialog = false;
    }
}
