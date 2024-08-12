using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [Header("Itens level 1")]
    public GameObject FirstCat;
    public GameObject catDialog;
    public GameObject catDialog2;

    [Header("DefaultPositionPortal")]
    public Vector3 homePosition;

    private Vector3 position;

    public static GameController instance = null;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //playGame();

        if(catDialog2 != null)
            enableDialog(catDialog2, false);
    }

    //PLAAAAAAAY
    public void playGame(){
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null){
            position = new Vector3(save.playerPosition[0], save.playerPosition[1], save.playerPosition[2]);
            SceneManager.LoadScene(save.level);
            //SceneManager.sceneLoaded += OnSceneLoadedSave;
            //setPlayerLvl(save.level, savePosition);
        }
    }


    public static int getLevelIndex() {
        string level = SceneManager.GetActiveScene().name;
        string levelNumber = level.Substring(level.Length - 1);
        return int.Parse(levelNumber) - 1; //arrays comecam em 0...
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
            FindObjectOfType<SaveLoad>().savePlayerPosition(exit, orientation, index);
        }
    }

    public void setPlayerInHome(){
        SceneManager.LoadScene("Level1");
        SceneManager.sceneLoaded += OnSceneLoadedForHome;
    }

    private void OnSceneLoadedForHome(Scene scene, LoadSceneMode mode){
        Transform player = FindObjectOfType<InteractionsController>().transform;
        if (player != null)
            player.position = new Vector3(homePosition.x, homePosition.y, homePosition.z);
        print("missao concluida!");

        SceneManager.sceneLoaded -= OnSceneLoadedForHome;
    }

    public void setPlayerInForest(){
        Save save = FindObjectOfType<SaveLoad>().loadGame();
        if (save != null){
            int index = save.previousLevel;
            SceneManager.sceneLoaded += (scene, mode) => OnSceneLoadedForForest(scene, mode, save.playerPosition, save.orientation);
            SceneManager.LoadScene(index);
        }
    }
    private void OnSceneLoadedForForest(Scene scene, LoadSceneMode mode, float[] playerPosition, int rotation){
        Vector3 newPosition = new Vector3(playerPosition[0], playerPosition[1], playerPosition[2]);
        Transform player = FindObjectOfType<InteractionsController>().transform;
        if (player != null){
            player.position = newPosition;
            player.rotation = Quaternion.Euler(player.rotation.eulerAngles.x, rotation, player.rotation.eulerAngles.z);
        }
            
        SceneManager.sceneLoaded -= (scene, mode) => OnSceneLoadedForForest(scene, mode, playerPosition, rotation);
    }


    //CONTROLE DE LVL E POSITION
    public int getPlayerLvl(){
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void OnSceneLoadedSave(Scene scene, LoadSceneMode mode){
        //setPlayerPosition(position);
        SceneManager.sceneLoaded -= OnSceneLoadedSave;
    }

    public Vector3 getPlayerPosition(){
        return FindObjectOfType<MovementController>().transform.position;
    }

    public void setPlayerPosition(Vector3 position){
        FindObjectOfType<MovementController>().transform.position = position;
    }

    //TUTORIAL
    public void enableTutorialCat(bool state)
    {
        FirstCat?.SetActive(state);
    }

    public void enableDialog(GameObject dialog, bool stts)
    {
        dialog?.SetActive(stts);
    }
}
