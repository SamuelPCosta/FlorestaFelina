using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMission : UIController
{
    [Header("Attributes")]
    public GameObject missionHud;
    public GameObject missionTitle;
    public GameObject missionDescription;

    [Header("Alternative text")]
    [Tooltip("Pocoes ja craftadas")]
    public string stage2AlternativeText = "Medique o gato com";

    private MissionType CurrentMission;

    public void disableMissionHUD()
    {
        missionHud.SetActive(false);
    }

    public void setMission(MissionType newMission)
    {
        CurrentMission = newMission;
        missionHud.SetActive(true);
        missionTitle.GetComponent<TextMeshProUGUI>().text = CurrentMission.title;
        print("tua missao eh: " + CurrentMission.title);
    }

    public void setMissionStage(int index){
        if (!missionHud.activeSelf)
            return;

        missionDescription.GetComponent<TextMeshProUGUI>().text = CurrentMission.description[index];
    }

    public void setAlternativeText(string potion){
        if (!missionHud.activeSelf)
            return;

        missionDescription.GetComponent<TextMeshProUGUI>().text = stage2AlternativeText +" "+ potion;
    }

    public void completeMission()
    {
        disableMissionHUD();
    }
}
