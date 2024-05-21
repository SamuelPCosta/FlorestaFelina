using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public enum Symptoms { THIRST, PAIN, INJURED, VERY_INJURED }

    public static int catWaterConsumption;
    public int _catWaterConsumption;
    [SerializeField] public Symptoms symptoms = new Symptoms();


    private bool analyzed = false;

    private void Awake()
    {
        catWaterConsumption = _catWaterConsumption;
    }

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getAnalyzedStts()
    {
        return analyzed;
    }

    public void analyzeCat()
    {
        analyzed = true;

        switch (symptoms)
        {
            case Symptoms.THIRST:
                FindObjectOfType<GameController>().setMission(GameController.Mission.MISSION1);
                break;
            case Symptoms.PAIN:
                FindObjectOfType<GameController>().setMission(GameController.Mission.MISSION2);
                break;
            case Symptoms.INJURED:
                FindObjectOfType<GameController>().setMission(GameController.Mission.MISSION3);
                break;
            case Symptoms.VERY_INJURED:
                FindObjectOfType<GameController>().setMission(GameController.Mission.MISSION4);
                break;
            default:
                FindObjectOfType<GameController>().setMission(GameController.Mission.MISSION1);
                break;

        }
    }
}
