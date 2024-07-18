using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsStatesController : MonoBehaviour
{
    private Save save;
    [Header("Cats")]
    public GameObject[] cats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MISSION_STATE getMissionState()
    {
        int index = getIndex();
        if (index == -1)
            return MISSION_STATE.NOT_STARTED;
        return save.missionState[index];
    }

    public bool checkMissionState(GameObject cat, MISSION_STATE state){
        save = FindObjectOfType<SaveLoad>().loadGame();
        if (save == null)
            return false;
        for (int i = 0; i < cats.Length; i++){
            if(cat == cats[i])
                return save.missionState[i] == state;
        }
        return false;
    }

    public void setMissionState(GameObject cat, MISSION_STATE state){
        if (save == null)
            return;
        for (int i = 0; i < cats.Length; i++){
            if (cat == cats[i])
                FindObjectOfType<SaveLoad>().saveMissionState(i, state);
        }
    }

    public string getName()
    {
        int index = getIndex();
        return save.catsNames[index];
    }

    //public string getVariation()
    //{
    //    return save.catsNames[index];
    //}

    public Vector3 getCatPosition()
    {
        int index = getIndex();
        return new Vector3(save.catsPosition[index,0], save.catsPosition[index, 1], save.catsPosition[index, 2]);
    }

    private int getIndex(){
        int ret = 0;
        if (save == null)
            return -1;
        for (int i = 0; i < cats.Length; i++){
            if (save.missionState[i] != MISSION_STATE.NOT_STARTED)
                ret++;
            else
                break;
        }
        return ret;
    }
}
