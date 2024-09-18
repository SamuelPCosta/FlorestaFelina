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
    public Texture2D[] pelage;

    private int numColors;
    private int numColorVariation;
    private int eyesVariation;

    private void Start()
    {
        createMaterial();
        //InvokeRepeating("drawAnimation", 6f, 6f);
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
        numColorVariation = Random.Range(0, 4);
        //numColorVariation = (numColors == 1 || numColors == 2) ? Random.Range(0, 3) : 0;
        //eyesVariation = Random.Range(0, 2);

        //furPattern.SetInt("_numColors", numColors);
        //furPattern.SetInt("_numColorVariation", numColorVariation);
        //furPattern.SetInt("_eyesVariation", eyesVariation);
        furPattern.SetTexture("_PelageMask", pelage[numColorVariation]);
        if (gameObject.activeSelf)
            transform.GetChild(1).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = furPattern;
    }

    public void eat()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("isEating");
    }

    public void caressReaction()
    {
        ParticleSystem hearts =  transform.GetChild(2).GetComponent<ParticleSystem>();
        //hearts.Stop();
        //hearts.Clear();
        hearts.Play();
    }

    public void drawAnimation()
    {
        Animator animator = GetComponent<Animator>();
        float[] values = { 0f, 0.25f, 0.5f, 0.75f };
        int index = Random.Range(0, values.Length);
        animator.SetFloat("idle", values[index]);
    }

    public Vector3Int getVariation(){
        return new Vector3Int(numColors, numColorVariation, eyesVariation);
    }

    public int getIndex()
    {
        return (int)sequence;
    }
}
