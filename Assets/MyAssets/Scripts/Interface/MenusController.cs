using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;
using TMPro;

public class MenusController : MonoBehaviour{

    private static int level1 = 1;
    private GameObject option = null;
    private GameObject btnCurrent = null;
    public FMOD.Studio.VCA masterVCA;
    private float oldVolume = 0f;

    private Vector3 position;

    [Header("Buttons")]
    public Button[] buttons;
    protected GameObject[] options;

    private PauseController pauseController = null;

    private bool sound = false;

    protected UIButtons _UIButtons;

    [Header("volume")]
    public Slider volumeSlider;

    [Header("camera")]
    public Slider sensibilitySlider;

    [Header("Loading")]
    public GameObject loadingScreen;
    public Slider slider;

    [Header("DefaultOp")]
    public GameObject defaultOp;

    [Header("Parent")]
    public GameObject MenuParent;

    public GameObject demoAlert;
    public GameObject fakeDel;

    //ACTIONS
    protected Inputs input;
    protected InputAction confirmOption;
    private InputAction move;

    private Vector2 lastMousePosition;
    private InteractionsController interactionsController = null;
    bool isOnMenu = false;
    bool isOnPause = false;

    protected void Awake()
    {
        masterVCA = RuntimeManager.GetVCA("vca:/VCA");
        float savedVolume = PlayerPrefs.GetInt("volume", 100);
        volumeSlider.value = savedVolume;

        float volume = MapValue(savedVolume, 0, 100, 0f, 1f);
        masterVCA.setVolume(volume);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);


        float savedSensibility = PlayerPrefs.GetFloat("sensibility", 0.5f);
        sensibilitySlider.value = savedSensibility;
        sensibilitySlider.onValueChanged.AddListener(OnSensibilityChanged);
        PlayerPrefs.Save();

        input = new Inputs();
        confirmOption = input.Player.ConfirmOption;
        move = input.Player.Move;

        options = new GameObject[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
            options[i] = buttons[i].gameObject;
    }

    public static MenusController instance = null;
    private void Start(){
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null){
            if (SceneManager.GetActiveScene().name.Equals("_MainMenu")){
                buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Start";
                if (buttons.Length > 5)
                    checkDelete(true);
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Equals("_MainMenu") && (buttons.Length > 5)){
                checkDelete(false);
            }
        }

        if (loadingScreen != null)
            loadingScreen?.SetActive(false);

        if (SceneManager.GetActiveScene().name.Equals("_MainMenu")){
            float aspectRatio = (float)Screen.width / (float)Screen.height;
            Vector3 scale = Vector3.one;
            if (aspectRatio >= 21.0f / 9.0f)
            {
                scale = Vector3.one;
            }
            else if (aspectRatio >= 16.0f / 9.0f)
            {
                scale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            GameObject.Find("BGMainMenu").GetComponent<RectTransform>().localScale = scale;
        }
        SetCursorState(false);
    }   

    public void checkDemoAlert()
    {
        Save temp = FindObjectOfType<SaveLoad>().loadGame();
        if (temp != null)
            demoAlert.SetActive(true);
        else demoAlert.SetActive(false);

        if (temp != null)
            fakeDel.SetActive(false);
        else fakeDel.SetActive(true);
    }

    private void Update()
    {
        

        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        if ((btnSelected == null || btnSelected.GetComponent<Button>() != null && !btnSelected.GetComponent<Button>().interactable) && btnCurrent != null)
        {
            if (!btnCurrent.name.Contains("Potion") && !btnCurrent.name.Contains("ButtonCat")){
                btnSelected = btnCurrent;
                EventSystem.current.SetSelectedGameObject(btnSelected);
            }
        }
        if (btnSelected != null && btnSelected.GetComponent<Button>() != null && btnSelected.GetComponent<Button>().interactable)
            btnCurrent = btnSelected;

        checkPause();
        if ((btnSelected != null && btnSelected.GetComponent<Slider>() != null)) 
            disableOptions();
    }

    void LateUpdate()
    {
        checkInput();
    }

    private void checkInput()
    {
        bool mouseMove = false;
        Vector2 currentMousePosition = new Vector2(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y);
        if (currentMousePosition != lastMousePosition){
            if (lastMousePosition != Vector2.zero)
                mouseMove = true;

            lastMousePosition = Mouse.current.position.ReadValue();
        }
        if (!SceneManager.GetActiveScene().name.Equals("_MainMenu")){
            interactionsController ??= FindObjectOfType<InteractionsController>();
            pauseController ??= FindObjectOfType<PauseController>();

            isOnMenu = interactionsController.isOnMenu;
            isOnPause = pauseController.isPaused;
            if (mouseMove){
                if(isOnMenu || isOnPause)
                    SetCursorState(true);
                else
                    SetCursorState(false);
            }

            if (!isOnMenu && !isOnPause)
            {
                SetCursorState(false);
            }
        }
        else if(mouseMove)
        {
            SetCursorState(true);
        }
        if (Gamepad.current != null){
            foreach (InputControl control in Gamepad.current.allControls){
                if (control.IsPressed())
                    SetCursorState(false);
            }
        }
    }

    public void SetCursorState(bool newState){
        Cursor.lockState = newState ? CursorLockMode.Confined : CursorLockMode.Locked;
        //Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.Locked;
    }

    private void checkPause(){
        string sceneName = SceneManager.GetActiveScene().name;
        bool pause = isPaused();
        if (Input.GetButtonDown("Cancel") && (sceneName.Equals("_MainMenu") || pause)){
            //excecao pause
            if (pause && MenuParent.transform.GetChild(2).gameObject.activeSelf){
                pauseController ??= FindObjectOfType<PauseController>();
                pauseController.pause(false);
                return;
            }

            int childCount = MenuParent.transform.childCount;
            for (int i = 2; i < childCount; i++)
                MenuParent.transform.GetChild(i).gameObject?.SetActive(i == 2); //Ativa somente o menu principal
            setCurrent(defaultOp);
        }
    }

    private bool isPaused(){
        pauseController ??= FindObjectOfType<PauseController>();
        if(pauseController == null)
            return false;
        else
            return pauseController.isPaused;
    }

    //PLAAAAAAAY
    public void playGame(){
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null && (new Vector3(save.playerPosition[0], save.playerPosition[1], save.playerPosition[2]) != Vector3.zero)){
            position = new Vector3(save.playerPosition[0], save.playerPosition[1], save.playerPosition[2]);
            if(save.level == 0)
                StartCoroutine(LoadSceneWithProgress(level1));
            else
                StartCoroutine(LoadSceneWithProgress(save.level));
            SceneManager.sceneLoaded += OnSceneLoadedForSave;
        }
        else
            StartCoroutine(LoadSceneWithProgress(level1));
    }

    private IEnumerator LoadSceneWithProgress(int sceneIndex) {
        Time.timeScale = 1;

        loadingScreen?.SetActive(true);
        slider.value = 0;
        yield return null;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone) {
            float progress = asyncLoad.progress;
            slider.value = progress;
            //Debug.Log($"Progresso: {progress * 100}%");

            yield return null;
        }

        SceneManager.sceneLoaded += OnSceneLoadedForSave;
    }

    public void deleteSave()
    {
        FindObjectOfType<SaveLoad>().DeleteSaveFile();
        checkDelete(false);
        //forceSelectOption(buttons[4].gameObject);
    }

    public void checkDelete(bool state)
    {
        buttons[5].enabled = state;
        buttons[5].interactable = state;
        buttons[5].GetComponent<CanvasGroup>().interactable = state;
        buttons[5].gameObject.SetActive(state);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnSceneLoadedForSave(Scene scene, LoadSceneMode mode){
        FindObjectOfType<InteractionsController>().transform.position = position;
        //StartCoroutine(SetPositionWhenReady());
        SceneManager.sceneLoaded -= OnSceneLoadedForSave;
        FindObjectOfType<GameController>().StartCoroutine("SetPositionWhenReady", position);
    }

    void OnVolumeChanged(float value){
        PlayerPrefs.SetInt("volume", (int)value);
        PlayerPrefs.Save();

        float volume = MapValue(value, 0, 100, 0f, 1f);
        masterVCA.setVolume(volume);
    }

    void OnSensibilityChanged(float value)
    {
        PlayerPrefs.SetFloat("sensibility", value);
        PlayerPrefs.Save();
        GameObject player = GameObject.Find("Player");
        if(player != null)
            player.GetComponent<MovementController>().getCameraSensibility();
    }

    public void selectOption(GameObject optionButton){
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        if ((btnSelected == null || !btnSelected.GetComponent<Button>().interactable) && btnCurrent != null)
        {
            btnSelected = btnCurrent;
            EventSystem.current.SetSelectedGameObject(btnSelected);
        }

        option = optionButton;
        disableOptions();
        
        getIndicator(optionButton).SetActive(true);
        TextMeshProUGUI select = optionButton.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>();
        select.color = ColorPalette.enableText;
    }

    public void forceSelectOption(GameObject optionBtn)
    {
        EventSystem.current.SetSelectedGameObject(optionBtn);
        option = optionBtn;
        disableOptions();

        getIndicator(option).SetActive(true);
        TextMeshProUGUI select = optionBtn.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>();
        select.color = ColorPalette.enableText;
    }

    private void disableOptions(){
        foreach (Button button in buttons){
            getIndicator(button.gameObject).SetActive(false);
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.color = ColorPalette.disableText;
        }
    }

    public void setCurrent(GameObject current)
    {
        EventSystem.current.SetSelectedGameObject(current);
    }

    private GameObject getIndicator(GameObject button)
    {
        return button.transform.GetChild(0).gameObject;
    }

    public void pauseAudio(bool state){
        if (state){
            masterVCA.getVolume(out oldVolume);
            masterVCA.setVolume(0);
        }
        else
        {
            masterVCA.setVolume(oldVolume);
        }
    }

    private float MapValue(float value, float fromMin, float fromMax, float toMin, float toMax){
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }
}
