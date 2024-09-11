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

    //ACTIONS
    protected Inputs input;
    protected InputAction confirmOption;
    private InputAction move;

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
        if (save != null)
        {
            if(SceneManager.GetActiveScene().name.Equals("_MainMenu"))
                buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Start";
            if(buttons.Length >5)
                buttons[5].enabled = true;
        }
        else
        {
            if (buttons.Length > 5)
                buttons[5].enabled = false;
        }

        if (loadingScreen != null)
            loadingScreen.GetComponent<CanvasGroup>().alpha = 0;

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
    }   

    private void Update()
    {
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        if ((btnSelected == null || btnSelected.GetComponent<Button>() != null && !btnSelected.GetComponent<Button>().interactable) && btnCurrent != null)
        {
            if (!btnCurrent.name.Contains("Potion")){
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
        //loadingScreen?.SetActive(true);
        loadingScreen.GetComponent<CanvasGroup>().alpha = 1;
        //slider.value = 0;
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
