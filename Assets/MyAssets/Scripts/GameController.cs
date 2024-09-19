using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{

    private static int level1 = 1;

    public MissionType mission = null;

    [Header("DefaultPositionPortal")]
    public Vector3 homePosition;

    [Header("CameraGuiada")]
    public bool isGuidedCamera = false;
    public bool dialog = false;

    private Vector3 position;

    public CatController currentCat = null;

    public static GameController instance = null;
    void Start(){
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name.Equals("Level1") && mission != null) { 
            FindObjectOfType<UIMission>().completeMission(mission);
            mission = null;
        }
    }

    public void scheduleResetCam(float duration)
    {
        Invoke("resetGuidedCam", duration);
    }

    public void resetGuidedCam()
    {
        FindObjectOfType<InteractionsController>().enableMovement = true;
        isGuidedCamera = false;
    }

    public static int getLevelIndex() {
        string level = SceneManager.GetActiveScene().name;
        string levelNumber = level.Substring(level.Length - 1);
        return int.Parse(levelNumber) - 1; //arrays comecam em 0...
    }

    public IEnumerator SetPositionWhenReady(Vector3 position){
        InteractionsController interactionsController = null;

        while (interactionsController == null)
        {
            interactionsController = FindObjectOfType<InteractionsController>();
            yield return null;
        }

        interactionsController.transform.position = position;
    }


    //CONTROLE DE CENA
    public void nextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void newChance(int scene){
        SceneManager.LoadScene(scene);
    }


    //CONTROLE DE PORTAIS
    public void savePlayerPortalPosition(Transform exit, int orientation){
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null){
            int index = SceneManager.GetActiveScene().buildIndex;
            FindObjectOfType<SaveLoad>().savePlayerPositionPortal(exit, orientation, index);
        }
    }

    public void setPlayerInHome(){
        SceneManager.LoadScene(level1);
        SceneManager.sceneLoaded += OnSceneLoadedForHome;
    }

    private void OnSceneLoadedForHome(Scene scene, LoadSceneMode mode){
        Transform player = FindObjectOfType<InteractionsController>().transform;
        if (player != null) { 
            player.position = new Vector3(homePosition.x, homePosition.y, homePosition.z);
            player.rotation = Quaternion.Euler(player.rotation.eulerAngles.x, 45f, player.rotation.eulerAngles.z);
            AudioController.missionComplete();
            FindObjectOfType<FeedbackController>().StopVibration();
        }
        SceneManager.sceneLoaded -= OnSceneLoadedForHome;
    }

    public void setPlayerInForest(){
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null){
            int index = save.previousLevel;
            if (index <= 0)
                return;
            AudioController.playAction(INTERACTIONS.Portal);
            SceneManager.sceneLoaded += (scene, mode) => OnSceneLoadedForForest(scene, mode, save.playerPositionPortal, save.orientation);
            SceneManager.LoadScene(index);
        }
    }
    private void OnSceneLoadedForForest(Scene scene, LoadSceneMode mode, float[] playerPosition, int rotation){
        Vector3 newPosition = new Vector3(playerPosition[0], playerPosition[1], playerPosition[2]);
        Transform player = FindObjectOfType<InteractionsController>().transform;
        if (player != null){
            player.position = newPosition;
            player.rotation = Quaternion.Euler(player.rotation.eulerAngles.x, rotation, player.rotation.eulerAngles.z);
            FindObjectOfType<FeedbackController>().StopVibration();
        }
        FindObjectOfType<SaveLoad>().saveLevel(SceneManager.GetActiveScene().buildIndex);
        SceneManager.sceneLoaded -= (scene, mode) => OnSceneLoadedForForest(scene, mode, playerPosition, rotation);
    }


    //CONTROLE DE LVL E POSITION
    public int getPlayerLvl(){
        return SceneManager.GetActiveScene().buildIndex;
    }

    public Vector3 getPlayerPosition(){
        return FindObjectOfType<MovementController>().transform.position;
    }
}
