using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatsHomeController : MonoBehaviour{

    public GameObject[] cats;
    private Save save;

    // Start is called before the first frame update
    void Start()
    {
        save = FindObjectOfType<SaveLoad>().loadGame();
        checkCatIndex();
    }

    public void checkCatIndex(){
        MISSION_STATE currentState;
        MISSION_STATE lastState = MISSION_STATE.NOT_STARTED;
        MISSION_STATE homeState = MISSION_STATE.HOME;

        for (int i = 0; i < cats.Length; i++) {
            currentState = getMissionStateByIndex(i+1); //descontando o primeiro, q eh controlado de outra forma
            bool shouldActivate = false;

            if (i == 0)
                shouldActivate = (currentState == homeState);
            else
                shouldActivate = (lastState == homeState && currentState != homeState);

            cats[i]?.SetActive(shouldActivate);
            lastState = currentState;
        }
    }

    public MISSION_STATE getMissionStateByIndex(int index){
        if (save == null)
            return MISSION_STATE.NOT_STARTED;
        return save.missionState[index];
    }
}
