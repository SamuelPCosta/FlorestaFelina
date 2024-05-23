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

    public string[] mission1 = new string[3];
    public string[] mission2 = new string[3];
    public string[] mission3 = new string[3];
    public string[] mission4 = new string[3];

    private GameController.Mission CurrentMission;

    void Start()
    {
        missionHud.SetActive(false);
        //TODO: ver o save
    }

    public void setMission(GameController.Mission newMission)
    {
        CurrentMission = newMission;
        print("tua missao eh: " + CurrentMission);
        missionHud.SetActive(true);

        string mission = "";
        switch (CurrentMission)
        {
            case GameController.Mission.MISSION1:
                mission = "Combate à sede";
                break;
            case GameController.Mission.MISSION2:
                mission = "X";
                break;
            case GameController.Mission.MISSION3:
                mission = "Y";
                break;
            case GameController.Mission.MISSION4:
                mission = "Z";
                break;
        }
        missionTitle.GetComponent<TextMeshProUGUI>().text = title + mission;
    }

    public void setStageMission(int index)
    {
        if (!missionHud.activeSelf)
            return;

        string description = "";
        switch (CurrentMission)
        {
            case GameController.Mission.MISSION1:
                description = mission1[index];
                break;
            case GameController.Mission.MISSION2:
                description = mission2[index];
                break;
            case GameController.Mission.MISSION3:
                description = mission3[index];
                break;
            case GameController.Mission.MISSION4:
                description = mission4[index];
                break;
        }
        missionDescription.GetComponent<TextMeshProUGUI>().text = description;
    }

    public void completeMission()
    {
        missionHud.SetActive(false);
    }
}
