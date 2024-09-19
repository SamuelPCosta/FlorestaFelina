using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Itens level 1")]
    public GameObject FirstCat;
    public GameObject catDialog;
    public GameObject catDialog2;
    public GameObject nextLevelMission;

    private bool nextMission = false;

    void Start(){
        if (catDialog2 != null)
            enableDialog(catDialog2, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (nextMission) {
            if (GameObject.Find("MissionPanel") == null){ 
                nextLevelMission.SetActive(true);
                nextMission = false;
            }
        }
    }

    //TUTORIAL
    //public void enableTutorialCat(bool state){
    //    FirstCat?.SetActive(state);
    //}

    public void enableDialog(GameObject dialog, bool stts){
        dialog?.SetActive(stts);
    }

    public void enableNextLevelMission()
    {
        nextMission = true;
    }
}
