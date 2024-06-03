using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum Mission { MISSION1, MISSION2, MISSION3, MISSION4 }
    private int CurrentMission;
    private int CurrentStageMission;

    [Header("Itens level 1")]
    public GameObject FirstCat;
    public GameObject catDialog;


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

        FirstCat?.SetActive(false);

        //TODO: chamar save e save.currentMission
        //TODO: stageMission tb
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public static int getLevelIndex()
    {
        string level = SceneManager.GetActiveScene().name;
        string levelNumber = level.Substring(level.Length - 1);
        return int.Parse(levelNumber) - 1; //arrays comecam em 0...
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void newChance(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void enableFirstCat()
    {
        FirstCat.SetActive(true);
    }
    public void enableCatDialog(bool stts)
    {
        catDialog.SetActive(stts);
    }

    public void setMission(Mission mission)
    {
        switch (mission)
        {
            case Mission.MISSION1:
                CurrentMission = (int) Mission.MISSION1;
                break;
            case Mission.MISSION2:
                CurrentMission = (int) Mission.MISSION2;
                break;
            case Mission.MISSION3:
                CurrentMission = (int) Mission.MISSION3;
                break;
            case Mission.MISSION4:
                CurrentMission = (int) Mission.MISSION4;
                break;
        }
        CurrentStageMission = 0;

        //TODO: save CurrentMission e CurrentStageMission

        UIMission _UIMission = FindFirstObjectByType<UIMission>();
        _UIMission.setMission((Mission)CurrentMission);
        _UIMission.setStageMission(0);
    }

    public Mission getMission()
    {
        return (Mission) CurrentMission;
    }

    public int getStageMission()
    {
        return CurrentStageMission;
    }
}
