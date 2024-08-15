using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CatsStatesController : MonoBehaviour
{
    private Save save;
    [Header("Spawners")]
    public GameObject[] spawners;
    private GameObject[] cats;
    private GameObject[] summons;

    [Header("CatsLvl1")]
    public GameObject[] catsHome;

    // Start is called before the first frame update
    void Start(){
        cats = new GameObject[spawners.Length];
        summons = new GameObject[spawners.Length];

        CatSpawnerController spawner;
        for (int i = 0; i < spawners.Length; i++){
            if(spawners[i] != null) { 
                spawner = spawners[i].GetComponent<CatSpawnerController>();
                cats[i] = spawner.getCat();
                summons[i] = spawner.getSummon();
            }
        }

        save = FindObjectOfType<SaveLoad>().loadGame();
        if (save == null)
            print("erro");

        if (SceneManager.GetActiveScene().name.Equals("Level1"))
            checkCatIndexHome();
        if (!SceneManager.GetActiveScene().name.Equals("Level1"))
            checkCatIndex();

        for (int i = 0; i < 20; i++){
            print(i +" - "+ getMissionStateByIndex(i));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkCatIndex(){
        MISSION_STATE currentState;
        MISSION_STATE lastState = MISSION_STATE.NOT_STARTED;
        MISSION_STATE finalState = MISSION_STATE.HOME;

        for (int i = 0; i < spawners.Length; i++) {
            currentState = getMissionStateByIndex((int)cats[i].GetComponent<CatController>().sequence);
            bool shouldActivate = false;

            if (i == 0)
                shouldActivate = (currentState != finalState);
            else
                shouldActivate = (lastState == finalState && currentState != finalState);

            cats[i]?.SetActive(shouldActivate);

            if(summons[i] != null)
                summons[i]?.SetActive(shouldActivate);
            lastState = currentState;

            //Ativa marcador
            if (shouldActivate && (currentState == MISSION_STATE.FIRST_INTERACTION || currentState == MISSION_STATE.STARTED || currentState == MISSION_STATE.HEALED))
                FindObjectOfType<InteractionsController>().setMarker(cats[i]);
        }
    }

    
    public void checkCatIndexHome(){
        MISSION_STATE currentState;
        MISSION_STATE homeState = MISSION_STATE.HOME;

        for (int i = 1; i < catsHome.Length; i++) {
            currentState = getMissionStateByIndex(i);
            bool shouldActivate = (currentState == homeState);

            catsHome[i]?.SetActive(shouldActivate);
        }
    }

    public MISSION_STATE getMissionStateByIndex(int index){
        save = FindObjectOfType<SaveLoad>().loadGame();
        if (save == null)
            return MISSION_STATE.NOT_STARTED;
        return save.missionState[index];
    }

    public void setMissionState(int index, MISSION_STATE state){
        FindObjectOfType<SaveLoad>().saveMissionState(index, state);
        save = FindObjectOfType<SaveLoad>().loadGame();
    }

    public void setMissionState(MISSION_STATE state){
        int index = getCurrentMissionIndex();
        setMissionState(index, state);
    }

    public void setMissionStateByPortal(){
        for (int i = 1; i < cats.Length; i++) { 
            if (save.missionState[i] != MISSION_STATE.HOME) {
                FindObjectOfType<SaveLoad>().saveMissionState(i, MISSION_STATE.HOME);
                save = FindObjectOfType<SaveLoad>().loadGame();
                break;
            }
        }
    }

    //public void setMissionComplete(){
    //    for (int i = 0; i < cats.Length; i++)
    //        if (save.missionState[i] != MISSION_STATE.HOME && i != 0) {  //MISSION_STATE.NOT_STARTED
    //            setMissionState(i, MISSION_STATE.HOME);
    //            break;
    //        }
    //}

    public string getName()
    {
        int index = getCurrentMissionIndex();
        return save.catsNames[index];
    }

    //public string getVariation()
    //{
    //    return save.catsNames[index];
    //}

    public Vector3 getCatPosition()
    {
        int index = getCurrentMissionIndex();
        return new Vector3(save.catsPosition[index,0], save.catsPosition[index, 1], save.catsPosition[index, 2]);
    }

    private int getCurrentMissionIndex(){
        int ret = 0;
        save = FindObjectOfType<SaveLoad>().loadGame();
        if (save == null)
            return -1;
        for (int i = 0; i < cats.Length; i++)
            if (save.missionState[i] != MISSION_STATE.STARTED) //MISSION_STATE.NOT_STARTED
                ret++;
            else
                break;

        return Mathf.Min(ret, cats.Length - 1);
    }
}
