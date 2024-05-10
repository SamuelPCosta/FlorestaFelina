using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum Missions { WATER, POTION1, POTION2, POTION3 }
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

    public void setMission(Missions mission)
    {
        switch (mission)
        {
            case Missions.WATER:
                CurrentMission = (int) Missions.WATER;
                break;
            case Missions.POTION1:
                CurrentMission = (int)Missions.POTION1;
                break;
            case Missions.POTION2:
                CurrentMission = (int)Missions.POTION2;
                break;
            case Missions.POTION3:
                CurrentMission = (int)Missions.POTION3;
                break;
        }

        print("tua missao eh: " + CurrentMission);
    }
}
