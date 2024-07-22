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

    [Header("Panel Controllers")]
    public DialogController dialogController;
    public CatMenuController catMenuController;

    [Header("Cameras")]
    [Tooltip("CamerasController script")]
    public CamerasController camerasController;

    [Header("UITextIndicator")]
    public UITextIndicator _UITextIndicator;

    [Header("TutorialMovement")]
    public GameObject MovementTutorial;

    [Header("CatsStatesController")]
    public CatsStatesController catsStatesController;

    [Header("Barriers")]
    public Speeches riverBarrier;

    //PRIVATES
    private Inputs input;

    private bool _workebenchCam = false;
    private bool _ShopCam = false;
    private bool _catMenuInteraction = false;
    private GameObject catCamera = null;
    private GameObject workenchCamera = null;

    private bool enableMovement = true;

    private bool inDialog = false;
    private bool inDynamicDialog = false;
    private bool executeActionByDialog = true;
    private string nameOfTutorial = "";
    private bool unlock = false;

    private UICollect _UICollect;

    private bool fastMovementAllowed = false;

    //Cats Attributes
    private bool firstInteraction = false;
    private bool catAnalyzed = false;
    private bool catMissionStarted = false;

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

        updateCatsState();
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
        checkSummon();
        checkPortal();
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
                unlock = true;
            }
        }
        else{
            _UITextIndicator.enableIndicator(IndicatorText.BARRIER, false);
            unlock = false;
        }
            
    }

    private void updateCatsState()
    {
        catsStatesController.getMissionState();
    }


    //CONTROLA A INTERACAO COM GATOS
    private void checkCat(){
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Cat"));
        if (collider != null){
            fastMovementAllowed = false;

            CatController catController = collider.GetComponent<CatController>();
            catAnalyzed = catsStatesController.checkMissionState(catController.gameObject, MISSION_STATE.STARTED);

            //SET indicadores
            if (!_catMenuInteraction)
                _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, true);
            if (catAnalyzed){
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
                if(!_catMenuInteraction)
                    _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, true);
            }
            else if (catsStatesController.checkMissionState(catController.gameObject, MISSION_STATE.FIRST_INTERACTION))
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, true);

            //##################INTERACOES##################
            //CARINHO
            checkCaress(catController.gameObject);

            //ANALISE
            if (checkCatAnalysis(catController))
                return;

            //MENU DE INTERACAO
            checkCatMenu(catController.gameObject);

            //##################INTERACOES##################
            
        }else{
            //LONGE DE GATOS
            fastMovementAllowed = true;
            _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, false);
            _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, false);
            _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
        }
    }

    private void checkCaress(GameObject cat){
        if (showAffection.triggered && !_catMenuInteraction){ //impede carinho quando o menu esta aberto
            if (!firstInteraction){
                firstInteraction = catsStatesController.checkMissionState(cat, MISSION_STATE.NOT_STARTED);
                //seta state desse gato como first interaction
                catsStatesController.setMissionState(cat, MISSION_STATE.FIRST_INTERACTION);
            }

            print("carinho");
            //TODO: ativar animacao
        }
    }

    private bool checkCatAnalysis(CatController catController){
        if (menu.triggered && firstInteraction && !catAnalyzed){
            //seta state desse gato como iniciada
            catsStatesController.setMissionState(catController.gameObject, MISSION_STATE.STARTED);
            catAnalyzed = true;
            catController.analyzeCat();
            return true;
        }
        return false;
    }

    private void checkCatMenu(GameObject cat){
        catMissionStarted = catsStatesController.checkMissionState(cat, MISSION_STATE.STARTED);
        if (menu.triggered && catMissionStarted){
            InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
            if (_catMenuInteraction) {
                _catMenuInteraction = false;
                catMenuController.turnOff();
                enableMovement = true;
                inputsCursor.SetCursorState(true);
            }else {
                _catMenuInteraction = true;
                catMenuController.turnOn();
                enableMovement = false;
                catCamera = getCamera(cat);
                inputsCursor.SetCursorState(false);
                _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, false);
                _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, false);
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
            }
        }    
    }

    //CONTROLA A INTERACAO COM NPCs
    private void checkNPC(){
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("DialogReload"));
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
        string moveTutorial = "MoveTutorial";
        
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Tutorial"));
        if (collider != null){
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
            if (nameOfTutorial == moveTutorial)
                MovementTutorial.SetActive(true);

            //gatilho pra acoes
            if (executeActionByDialog)
                managerElements(nameOfTutorial);

            //gerenciar salvamento
            dialog.markDialog();

            fastMovementAllowed = false;
        }
        else{
            if (inDynamicDialog)
                inDynamicDialog = false;

            //Execao do tutorial de movimento
            Vector2 playerMovement = GetComponent<InputsMovement>().move;

            if (FindObjectOfType<DialogSave>().getDialogState(1) || nameOfTutorial.Equals(moveTutorial) && playerMovement != Vector2.zero && enableMovement)
                MovementTutorial.SetActive(false);

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

    //CONTROLA INTERACAO COM O SUMMON DA BANCADA E PORTAL
    private void checkSummon()
    {
        Collider summon = boxcast.checkProximity(LayerMask.NameToLayer("Summon"));
        if (summon != null){
            _UITextIndicator.enableIndicator(IndicatorText.SUMMON, true);
            if (makeWay.triggered){
                summon.GetComponent<SummonController>().summonStructures();
            }
        }
        else
            _UITextIndicator.enableIndicator(IndicatorText.SUMMON, false);
    }

    //CONTROLA INTERACAO COM O PORTAL
    private void checkPortal()
    {
        Collider portal = boxcast.checkProximity(LayerMask.NameToLayer("Portal"));
        if (portal != null){
            _UITextIndicator.enableIndicator(IndicatorText.PORTAL, true);
            if (makeWay.triggered)
            {
                portal.GetComponent<PortalController>().usePortal();
            }
        }
        else
            _UITextIndicator.enableIndicator(IndicatorText.PORTAL, false);
    }

    //CONTROLA INTERACAO COM A BANCADA
    private void checkWorkbench()
    {
        Collider workbench = boxcast.checkProximity(LayerMask.NameToLayer("Workbench"));
        if (workbench != null){
            if(!_workebenchCam)
                _UITextIndicator.enableIndicator(IndicatorText.WORKBENCH, true);
            if (menu.triggered){
                WorkbenchController workbenchController = GameObject.FindObjectOfType<WorkbenchController>();
                InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
                if (_workebenchCam){
                    _workebenchCam = false;
                    workbenchController.turnOff();
                    inputsCursor.SetCursorState(true);
                    enableMovement = true;
                }
                else{
                    _workebenchCam = true;
                    workbenchController.turnOnMenu();
                    workenchCamera = getCamera(workbench.gameObject);
                    inputsCursor.SetCursorState(false);
                    enableMovement = false;
                    Transform workbenchOrigin = workbench.transform.GetChild(1); //segundo filho
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
                FindObjectOfType<GameController>().nextScene();
        }
        else
            _UITextIndicator.enableIndicator(IndicatorText.GATE, false);
    }

    private void checkCameras(){
        //Controla trasicao de cameras
        RaycastHit hit;
        if (Physics.Raycast(PlayerRoot.transform.position, Vector3.down, out hit)){
            if (_workebenchCam)
                camerasController.ActivateDynamicCamera(workenchCamera);
            else if (!hit.collider.CompareTag("InnerRoom")){ //AREA EXTERNA
                if (!_catMenuInteraction){
                    if (hit.collider.CompareTag("EnvironmentView"))
                        camerasController.ActivateCamera(CamerasController.cam.Objective);
                    else if (hit.collider.CompareTag("EnvironmentViewDontReload"))
                        camerasController.ActivateCamera(CamerasController.cam.ObjectiveDontReload);
                    else
                        camerasController.ActivateCamera(CamerasController.cam.Default);
                }else
                    camerasController.ActivateDynamicCamera(catCamera);
            }
            else{ //AREA INTERNA
                 camerasController.ActivateCamera(CamerasController.cam.Close);
            }
            if(!_workebenchCam && !_catMenuInteraction) //reseta a prioridade das cameras dinamicas
                camerasController.DeactivateDynamicCamera(workenchCamera, catCamera);

            //MOVIMENTACAO BLOQUEADA
            if (hit.collider.CompareTag("EnvironmentView")
             || hit.collider.CompareTag("EnvironmentViewDontReload") || !enableMovement)
                transform.GetComponent<MovementController>().enablePlayerMovement(false);
            else
                transform.GetComponent<MovementController>().enablePlayerMovement(true);

        }
    }

    private GameObject getCamera(GameObject gameObject){
        return gameObject.transform.GetChild(0).gameObject;
    }

    private void managerElements(string name){
        GameController gameController = FindFirstObjectByType<GameController>();

        if (name.Equals("MoveTutorial")){
            gameController.enableDialog(gameController.catDialog, false);
            gameController.enableTutorialCat(false);
        }

        if (name.Equals("WorkbenchTutorial")){
            gameController.enableTutorialCat(true);
            gameController.enableDialog(gameController.catDialog, true);
        }

        if (name.Equals("NextActionDialog")){
            riverBarrier.markDialog();
            riverBarrier.gameObject.SetActive(false);
        }

        executeActionByDialog = false;
    }
}
