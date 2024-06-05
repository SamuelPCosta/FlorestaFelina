using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public enum Symptoms { THIRST, PAIN, INJURED, VERY_INJURED, TUTORIAL }

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
        Mission mission;

        switch (symptoms)
        {
            case Symptoms.THIRST:
                mission = Mission.THIRST;
                break;
            case Symptoms.PAIN:
                mission = Mission.MISSION3;
                break;
            case Symptoms.INJURED:
                mission = Mission.MISSION4;
                break;
            case Symptoms.VERY_INJURED:
                mission = Mission.MISSION5;
                break;
            default:
                mission = Mission.TUTORIAL;
                break;
        }

        FindObjectOfType<MissionController>().setMission(mission);
    }
}
