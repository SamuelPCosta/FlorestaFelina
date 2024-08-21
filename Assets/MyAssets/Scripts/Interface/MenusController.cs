using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;

public class MenusController : MonoBehaviour{
    private static int level1 = 1;
    private GameObject option = null;
    public FMOD.Studio.VCA masterVCA;

    private Vector3 position;

    [Header("Buttons")]
    public Button[] buttons;
    protected GameObject[] options;

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

    public enum MenuOption { OPTION1, OPTION2, OPTION3, OPTION4, OPTION5 };

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

        options = new GameObject[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
            options[i] = buttons[i].gameObject;
        //_UIButtons = GetComponent<UIButtons>();
        //_UIButtons.setButtons(buttons);
    }

    public static MenusController instance = null;
    private void Start(){

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        loadingScreen?.SetActive(false);
    }

    //PLAAAAAAAY
    public void playGame(){
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null && (new Vector3(save.playerPosition[0], save.playerPosition[1], save.playerPosition[2]) != Vector3.zero)){
            position = new Vector3(save.playerPosition[0], save.playerPosition[1], save.playerPosition[2]);
            if(save.level == 0)
                SceneManager.LoadScene(level1);
            else
                SceneManager.LoadScene(save.level);
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
        option = optionButton;
    }

    private float MapValue(float value, float fromMin, float fromMax, float toMin, float toMax){
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }
}
