using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatsStatesController : MonoBehaviour
{
    private Save save;
    [Header("Spawners")]
    public GameObject[] spawners;
    private GameObject[] cats;
    private GameObject[] summons;

    // Start is called before the first frame update
    void Start(){
        cats = new GameObject[spawners.Length];
        summons = new GameObject[spawners.Length];

        CatSpawnerController spawner;
        for (int i = 0; i < spawners.Length; i++){
            spawner = spawners[i].GetComponent<CatSpawnerController>();
            cats[i] = spawner.getCat();
            summons[i] = spawner.getSummon();
        }

        save = FindObjectOfType<SaveLoad>().loadGame();
        checkCatIndex();
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
            currentState = getMissionStateByIndex(i);
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
            if (shouldActivate && (currentState == MISSION_STATE.FIRST_INTERACTION || currentState == MISSION_STATE.STARTED || currentState == MISSION_STATE.FINISH))
                FindObjectOfType<InteractionsController>().setMarker(cats[i]);
        }
    }

    //public MISSION_STATE getMissionState()
    //{
    //    int index = getIndex();
    //    if (index == -1)
    //        return MISSION_STATE.NOT_STARTED;
    //    return save.missionState[index];
    //}

    //public bool checkMissionState(GameObject cat, MISSION_STATE state){
    //    save = FindObjectOfType<SaveLoad>().loadGame();
    //    if (save == null)
    //        return false;
    //    for (int i = 0; i < cats.Length; i++)
    //        if(cat == cats[i])
    //            return save.missionState[i] == state;

    //    return false;
    //}

    //public void setMissionState(GameObject cat, MISSION_STATE state){
    //    if (save == null)
    //        return;
    //    for (int i = 0; i < cats.Length; i++)
    //        if (cat == cats[i])
    //            FindObjectOfType<SaveLoad>().saveMissionState(i, state);
    //}

    public MISSION_STATE getMissionStateByIndex(int index)
    {
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
