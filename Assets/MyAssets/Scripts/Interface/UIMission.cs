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
    public GameObject missionCompleteIcon;
    public TextMeshProUGUI missionCompleteText;
    public GameObject refreshUI;

    [Header("Alternative text")]
    [Tooltip("Pocoes ja craftadas")]
    public string stage2AlternativeText = "Medique o gato com";

    private MissionType CurrentMission;

    private void Start()
    {
        missionCompleteIcon?.SetActive(false);
    }

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
        StopAllCoroutines();
        StartCoroutine(FadeInOut(refreshUI.GetComponent<CanvasGroup>(), 1f));
        missionDescription.GetComponent<TextMeshProUGUI>().text = CurrentMission.description[index];
    }

    public void setAlternativeText(string potion){
        if (!missionHud.activeSelf)
            return;
        StopAllCoroutines();
        StartCoroutine(FadeInOut(refreshUI.GetComponent<CanvasGroup>(), 1f));
        missionDescription.GetComponent<TextMeshProUGUI>().text = stage2AlternativeText +" "+ potion;
    }

    public void completeMission(MissionType mission)
    {
        disableMissionHUD();
        missionCompleteIcon.SetActive(true);

        CurrentMission = mission;
        missionCompleteText.text = CurrentMission.title;
        AudioController.missionComplete();
        Invoke("disableCompleteMission", 3);
    }

    private void disableCompleteMission()
    {
        missionCompleteIcon.SetActive(false);
    }

    IEnumerator FadeInOut(CanvasGroup canvas, float duration)
    {
        float elapsed = 0f;
        float max = .8f;

        while (elapsed < duration * 2)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.PingPong(elapsed, duration) / duration;
            canvas.alpha = Mathf.Lerp(0f, max, t);
            yield return null;
        }

        canvas.alpha = 0f;
    }
}
