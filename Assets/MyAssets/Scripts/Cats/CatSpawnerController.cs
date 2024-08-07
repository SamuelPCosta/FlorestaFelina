using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatSpawnerController : MonoBehaviour
{
    //RESPONSAVEL POR RANDOMIZAR ATRIBUTOS DO GATO

    [Header("Attributes")]
    [SerializeField] private CatController catController;
    [SerializeField] private GameObject summon;

    [Header("Variation")]
    public int maxFurrVariation = 4;
    public int maxPatternVariation = 4;
    [Tooltip("THIRST, PAIN, INJURED, VERY_INJURED")]
    public CatController.Symptoms[] symptoms = new CatController.Symptoms[4];

    void Start(){
        //TODO: setar catsVariations

        int catFurrVariation = Random.Range(0, maxFurrVariation);
        int catPatternVariation = Random.Range(0, maxPatternVariation);

        int catSymptom = Random.Range(0, symptoms.Length);
        catController.symptoms = symptoms[catSymptom];
    }


    public GameObject getCat()
    {
        return catController.gameObject;
    }

    public GameObject getSummon()
    {
        return summon;
    }

    //public SEQUENCE getOrder()
    //{
    //    return sequence;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
