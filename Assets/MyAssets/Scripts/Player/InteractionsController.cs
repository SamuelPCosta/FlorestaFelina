using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractionsController : MonoBehaviour
{       
    [Header("Player object")]
    public GameObject PlayerRoot;
    public ProximityController boxcast;
    public GameObject bag;
    public GameObject marker;
    public GameObject roomba;
    public GameObject roombaVFX;
    public GameObject roombaHips;
    public GameObject fruit;
    public GameObject waterParticles;
    public GameObject waterDrops;

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
    private UICollect _UICollect;

    private bool interactions = true;

    private bool _workebenchCam = false;
    //private bool _ShopCam = false;
    private bool _catMenuInteraction = false;
    private GameObject catCamera = null;
    private GameObject workenchCamera = null;

    private bool enableMovement = true;

    private bool inDialog = false;
    private bool inDynamicDialog = false;
    private bool executeActionByDialog = true;
    private string nameOfTutorial = "";
    private bool unlock = false;

    private bool fastMovementAllowed = false;
    private bool catInBag = false;
    private Transform currentCat = null;

    private ParticleSystem waves;
    private ParticleSystem drops;
    private Vector3 lastWavePosition = Vector3.zero;
    private bool isExitWave = false;

    //Cats Attributes
    private CatController tutorialCat = null;
    private bool catNotStarted = false;
    private bool catFirstInteraction = false;
    private bool catAnalyzed = false;
    private bool catHealed = false;
    private bool catHome = false;

    //ACTIONS
    private InputAction menu;
    private InputAction collect;
    private InputAction makeWay;
    private InputAction dialog;
    private InputAction moveFast;
    private InputAction nextLevel;
    private InputAction showAffection;
    private InputAction bagInput;

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
        bag?.SetActive(false);
        waterParticles?.SetActive(false);
        waterDrops.gameObject?.SetActive(false);
        waves = waterParticles.transform.GetChild(0).GetComponent<ParticleSystem>();
        drops = waterDrops.transform.GetChild(0).GetComponent<ParticleSystem>();

        menu = input.Player.Menu;
        collect = input.Player.Collect;
        makeWay = input.Player.MakeWay;
        dialog = input.Player.Dialog;
        moveFast = input.Player.MoveFast;
        nextLevel = input.Player.NextLevel;
        showAffection = input.Player.ShowAffection;
        bagInput = input.Player.Bag;

        catCamera = null;

        roomba?.SetActive(false);
        roombaHips?.SetActive(true);
        
        fruit?.SetActive(false);

        InputsMovement inputsCursor = GameObject.FindObjectOfType<InputsMovement>();
        inputsCursor.SetCursorState(true);

        //updateCatsState();
    }

    public void setInteractions()
    {
        interactions = !interactions;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactions) { 
            bool isCollectible = checkCollectibles();
            bool isPage = checkNewPage();
            bool isFruit = checkFruit();

            if(!isCollectible && !isPage && !isFruit)
                _UITextIndicator.enableIndicator(IndicatorText.COLLECT, false);

            checkWay();
            checkNPC();
            checkDialogs();
            checkMarker();
            checkCat();
            checkWorkbench();
            checkSummon();
            checkPortal();
            checkGate();
            checkCameras();

            Vector2 move = transform.GetComponent<InputsMovement>().move;

            //Controla particulas do roomba
            if (moveFast.triggered && fastMovementAllowed && !(inDialog || inDynamicDialog)) { 
                transform.GetComponent<MovementController>().changeLocomotion();
                roomba?.SetActive(!roomba.activeSelf);
                roombaHips?.SetActive(!roombaHips.activeSelf);
            }
            if(roomba.activeSelf)
                if (move != Vector2.zero)
                    roombaVFX?.SetActive(true);
                else
                    roombaVFX?.SetActive(false);

            //Controla particulas da agua
            if (isExitWave)
                waterParticles.transform.position = lastWavePosition;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            CancelInvoke("DisableWaves");
            isExitWave = false;
            waterParticles.transform.position = gameObject.transform.position;
            waterParticles?.SetActive(true);
            waterDrops?.SetActive(true);
            //waves.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            drops.Play();
            waves.Play();
            fastMovementAllowed = false;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            Vector3 particlesPosition = gameObject.transform.position;
            if (waterParticles.activeSelf) { 
                particlesPosition = gameObject.transform.position;
                Vector2 move = transform.GetComponent<InputsMovement>().move;

                var velocityOverLifetime = waves.velocityOverLifetime;
                if (move != Vector2.zero && !roomba.activeSelf)
                    velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(-0.4f); //Velocidade do rastro
                else if(move != Vector2.zero && roomba.activeSelf) 
                    velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(-1f); //roomba na agua
                else
                    velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(0f);

                //TODO: SOM NA AGUA
                if (move == Vector2.zero) { 
                    drops.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
                else if (!drops.isPlaying) { 
                    drops.Play();
                }
            }
            waterParticles.transform.position = particlesPosition;
            fastMovementAllowed = false;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            waves.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            isExitWave = true;
            lastWavePosition = waterParticles.transform.position;
            Invoke("DisableWaves", 2f);
            Invoke("DisableDrops", .8f);
            fastMovementAllowed = true;
        }
    }

    private void DisableWaves(){
        waterParticles?.SetActive(false);
        isExitWave = false;
    }

    private void DisableDrops(){
        drops.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    //CONTROLA COLETA E CONEXAO COM INVENTARIO
    private bool checkCollectibles()
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
        }
        return (collider != null);
    }

    private bool checkNewPage(){
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("NewPage"));
        if (collider != null){
            _UITextIndicator.enableIndicator(IndicatorText.COLLECT, true);
            if (collect.triggered){
                FindObjectOfType<JournalController>().addPage();
                Destroy(collider.gameObject);
                //TODO: SAVE
                _UICollect.spawnCollectedPage();
            }
        }
        return (collider != null);
    }

    private bool checkFruit(){
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Fruit"));
        if (collider != null){
            _UITextIndicator.enableIndicator(IndicatorText.COLLECT, true);
            if (collect.triggered){
                Destroy(collider.gameObject);
                fruit?.SetActive(true);
                //TODO: SAVE
                _UICollect.spawnCollectedFruit();
            }
        }
        return (collider != null);
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

    public void setMarker(GameObject cat)
    {
        if (cat == null) { 
            currentCat = null;
            return;
        }
        currentCat = cat.transform;
    }

    private void checkMarker(){
        int distance = 3;
        if (currentCat != null && Vector3.Distance(transform.position, currentCat.transform.position) > distance) {
            marker?.SetActive(true);
            marker.transform.LookAt(currentCat);
            marker.transform.eulerAngles = new Vector3(-90f, marker.transform.eulerAngles.y, marker.transform.eulerAngles.z);
        }else
            marker?.SetActive(false);
    }

    //CONTROLA A INTERACAO COM GATOS
    private void checkCat(){
        Collider collider = boxcast.checkProximity(LayerMask.NameToLayer("Cat"));
        if (collider != null){
            fastMovementAllowed = false;

            CatController catController = collider.GetComponent<CatController>();
            MISSION_STATE state = catsStatesController.getMissionStateByIndex(catController.getIndex());
            Mission missionType = FindObjectOfType<MissionController>().getMission();

            catNotStarted = state == MISSION_STATE.NOT_STARTED;
            catFirstInteraction = state == MISSION_STATE.FIRST_INTERACTION;
            catAnalyzed = state == MISSION_STATE.STARTED;
            catHealed = state == MISSION_STATE.HEALED;
            catHome = state == MISSION_STATE.HOME;

            //SET indicadores
            if (!_catMenuInteraction)
                _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, true);
            if (!catFirstInteraction){
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
                if((catAnalyzed || catHealed || catHome) && !_catMenuInteraction)
                    _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, true);
            }
            else if (catFirstInteraction)
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, true);
            else if(catHealed && missionType != Mission.TUTORIAL) //TODO: indicador da bolsa
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, true);
            
            //##################INTERACOES##################
            //CARINHO
            checkCaress(catController);

            //ANALISE
            if (checkCatAnalysis(catController))
                return;

            //MENU DE INTERACAO
            checkCatMenu(catController);

            //CARREGA GATO
            checkCatOnTheBag(catController);

            //##################INTERACOES##################

        }
        else{
            //LONGE DE GATOS
            fastMovementAllowed = true;
            _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, false);
            _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, false);
            _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
        }
    }

    private void checkCaress(CatController cat){
        if (showAffection.triggered && !_catMenuInteraction){ //impede carinho quando o menu esta aberto
            if (catNotStarted){
                //seta state desse gato como first interaction
                catsStatesController.setMissionState(cat.getIndex(), MISSION_STATE.FIRST_INTERACTION);
                setMarker(cat.gameObject);
            }

            print("carinho no gato "+cat);
            //TODO: ativar animacao
        }
    }

    private bool checkCatAnalysis(CatController catController){
        if (menu.triggered && catsStatesController.getMissionStateByIndex(catController.getIndex()) == MISSION_STATE.FIRST_INTERACTION){    
            //seta state desse gato como iniciada
            catsStatesController.setMissionState(catController.getIndex(), MISSION_STATE.STARTED);
            catAnalyzed = true;
            catController.analyzeCat();
            return true;
        }
        return false;
    }

    private void checkCatMenu(CatController cat){
        if (menu.triggered && (catAnalyzed || catHealed)){
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
                catCamera = getCamera(cat.gameObject);
                inputsCursor.SetCursorState(false);
                _UITextIndicator.enableIndicator(IndicatorText.CAT_MENU, false);
                _UITextIndicator.enableIndicator(IndicatorText.CAT_AFFECTION, false);
                _UITextIndicator.enableIndicator(IndicatorText.CAT_ANALYSE, false);
            }
        }    
    }

    private void checkCatOnTheBag(CatController cat){
        Mission missionType = FindObjectOfType<MissionController>().getMission();
        if (bagInput.triggered && catHealed && !_catMenuInteraction && missionType != Mission.TUTORIAL){  //impede acao quando o menu esta aberto e qndo é o gato do tutorial
            bag?.SetActive(true);
            cat.gameObject.SetActive(false);
            catInBag = true;
            setMarker(null);
        }else 
        if (catHealed && missionType == Mission.TUTORIAL){  //desliga marcador do TUTORIAL
            tutorialCat = cat;
            setMarker(null);
        }
    }

    public bool isCatInBag(){
        return catInBag;
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
        }
        else
            _UITextIndicator.enableIndicator(IndicatorText.NPC, false);
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
                MovementTutorial?.SetActive(true);

            //gatilho pra acoes
            if (executeActionByDialog)
                managerElements(nameOfTutorial);

            //gerenciar salvamento
            dialog.markDialog();
        }
        else{
            //Execao do tutorial de movimento
            Vector2 playerMovement = GetComponent<InputsMovement>().move;

            if (FindObjectOfType<DialogSave>().getDialogState(1) || nameOfTutorial.Equals(moveTutorial) && playerMovement != Vector2.zero && enableMovement)
                MovementTutorial?.SetActive(false);

            executeActionByDialog = true;
        }
    }

    public void exitDialog()
    {
        inDialog = false;
        if (inDynamicDialog)
            inDynamicDialog = false;
        //inDynamicDialog = false;
        fastMovementAllowed = true;
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
            //TODO: se o portal é forest e a posicao de saida é zero return;
            //FindObjectOfType<GameController>().
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
            else{ //AREA INTERNA (CLOSE)
                if(_catMenuInteraction)
                    camerasController.ActivateDynamicCamera(catCamera);
                else
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

        if (name.Equals("MoveTutorial") && gameController.catDialog != null){
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
            catsStatesController.setMissionState(tutorialCat.getIndex(), MISSION_STATE.HOME);
            MissionController missionController = FindObjectOfType<MissionController>();
            missionController.addStage();
            missionController.checkMissionCompletion();
        }

        executeActionByDialog = false;
    }
}
