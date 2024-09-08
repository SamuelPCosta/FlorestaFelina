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
    }   

    private void Update()
    {
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        if ((btnSelected == null || !btnSelected.GetComponent<Button>().interactable) && btnCurrent != null)
        {
            btnSelected = btnCurrent;
            EventSystem.current.SetSelectedGameObject(btnSelected);
        }
        if (btnSelected != null && btnSelected.GetComponent<Button>().interactable)
            btnCurrent = btnSelected;

        checkPause();
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

        float volume = MapValue(value, 0, 100, 0f, 1f);
        masterVCA.setVolume(volume);
    }

    public void selectOption(GameObject optionButton){
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        if ((btnSelected == null || !btnSelected.GetComponent<Button>().interactable) && btnCurrent != null)
        {
            btnSelected = btnCurrent;
            EventSystem.current.SetSelectedGameObject(btnSelected);
        }

        option = optionButton;
        foreach (Button button in buttons) { 
            getIndicator(button.gameObject).SetActive(false);
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.color = ColorPalette.disableText;
        }
        getIndicator(optionButton).SetActive(true);
        TextMeshProUGUI select = optionButton.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>();
        select.color = ColorPalette.enableText;
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
