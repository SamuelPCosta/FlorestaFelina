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

    public Material catMaterial;

    private void Start()
    {
        createMaterial();
    }

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

    private void createMaterial(){
        Material furPattern = new Material(catMaterial);

        //SORTEIO DE INT EH MaxEXCLUSIVEEE
        int numColors = Random.Range(1, 4);
        int numColorVariation = (numColors == 1 || numColors == 2) ? Random.Range(0, 3) : 0;
        int eyesVariation = Random.Range(0, 2);

        furPattern.SetInt("_numColors", numColors);
        furPattern.SetInt("_numColorVariation", numColorVariation);
        furPattern.SetInt("_eyesVariation", eyesVariation);
        if(gameObject.activeSelf)
            transform.GetChild(1).GetComponent<Renderer>().material = furPattern;
    }

    public int getIndex()
    {
        return (int)sequence;
    }
}
