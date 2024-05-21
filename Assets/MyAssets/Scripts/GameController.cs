using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum Mission { MISSION1, MISSION2, MISSION3, MISSION4 }
    public int CurrentMission;

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
      
        //TODO: chamar save e save.currentMission
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

        //TODO: save CurrentMission

        print("tua missao eh: " + CurrentMission);
    }

    public Mission getMission()
    {
        return (Mission) CurrentMission;
    }
}
