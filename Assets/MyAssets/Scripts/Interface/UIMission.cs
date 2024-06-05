using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMission : UIController
{
    public GameObject missionHud;
    public GameObject missionTitle;
    public GameObject missionDescription;

    public string title = "Missão: ";

    private MissionType CurrentMission;

    void Start()
    {
        //missionHud.SetActive(false);
        //TODO: ver o save
    }

    public void disableMissionHUD()
    {
        missionHud.SetActive(false);
    }

    public void setMission(MissionType newMission)
    {
        CurrentMission = newMission;
        missionHud.SetActive(true);
        missionTitle.GetComponent<TextMeshProUGUI>().text = title + CurrentMission.title;
        print("tua missao eh: " + CurrentMission.title);
    }

    public void setMissionStage(int index)
    {
        if (!missionHud.activeSelf)
            return;

        missionDescription.GetComponent<TextMeshProUGUI>().text = CurrentMission.description[index];
    }

    public void completeMission()
    {
        disableMissionHUD();
    }
}
