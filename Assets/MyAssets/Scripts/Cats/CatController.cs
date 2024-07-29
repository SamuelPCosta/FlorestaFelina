using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SEQUENCE
{
    First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth,
    Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth, Sixteenth, Seventeenth, Eighteenth, Nineteenth, Twentieth
}

public class CatController : MonoBehaviour
{

    [SerializeField] private SEQUENCE sequence;
    public enum Symptoms { THIRST, PAIN, INJURED, VERY_INJURED, TUTORIAL }

    public static int catWaterConsumption;
    [SerializeField] public Symptoms symptoms = new Symptoms();
    public int _catWaterConsumption;

    private void Awake()
    {
        catWaterConsumption = _catWaterConsumption;
    }

    public void analyzeCat(){
        Mission mission;

        switch (symptoms){
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

    public int getIndex()
    {
        return (int)sequence;
    }
}
