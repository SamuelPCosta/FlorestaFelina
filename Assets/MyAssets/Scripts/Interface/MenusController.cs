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

    private bool sound = false;

    //[Header("Menu")]
    //public GameObject menu;

    protected UIButtons _UIButtons;

    [Header("volume")]
    public Slider volumeSlider;

    [Header("Loading")]
    public GameObject loadingScreen;
    public Slider slider;

    //ACTIONS
    protected Inputs input;
    protected InputAction confirmOption;
    private InputAction move;
    private float move_x;

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
        //_UIButtons = GetComponent<UIButtons>();
        //_UIButtons.setButtons(buttons);
    }

    public static MenusController instance = null;
    private void Start(){

        //if (instance == null)
        //    instance = this;
        //else
        //    Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null)
        {
            buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        }


        if (loadingScreen != null)
            loadingScreen?.SetActive(false);
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
        //print(EventSystem.current.currentSelectedGameObject);
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen?.SetActive(true);

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
        SceneManager.sceneLoaded -= OnSceneLoadedForSave;
    }


    void OnVolumeChanged(float value){
        PlayerPrefs.SetInt("volume", (int)value);

        float volume = MapValue(value, 0, 100, 0f, 1f);
        masterVCA.setVolume(volume);
    }

    public void selectOption(GameObject optionButton){
        //print("a");

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

    public void pauseAudio(bool state)
    {
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
