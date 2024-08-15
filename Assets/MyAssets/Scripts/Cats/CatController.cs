using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SEQUENCE
{
    First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth,
    Eleventh, Twelfth, Thirteenth, Fourteenth, Fifteenth, Sixteenth, Seventeenth, Eighteenth, Nineteenth, Twentieth
}

public class CatController : MonoBehaviour{

    [SerializeField] public SEQUENCE sequence;
    public enum Symptoms { THIRST, PAIN, INJURED, VERY_INJURED, TUTORIAL }

    [SerializeField] public Symptoms symptoms = new Symptoms();

    public void analyzeCat(){
        Mission mission;

        switch (symptoms){
            case Symptoms.THIRST:
                mission = Mission.THIRST;
                break;
            case Symptoms.PAIN:
                mission = Mission.PAIN;
                break;
            case Symptoms.INJURED:
                mission = Mission.INJURED;
                break;
            case Symptoms.VERY_INJURED:
                mission = Mission.VERY_INJURED;
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
