using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatSpawnerController : MonoBehaviour
{
    [Header("Attributes")]
    public CatController catController;

    [Header("Variation")]
    public int maxFurrVariation = 4;
    public int maxPatternVariation = 4;
    public bool[] symptoms = new bool[4];

    void Start(){
        int arrayLength = symptoms.Length;
        //TODO: setar catsVariations

        int catFurrVariation = Random.Range(0, maxFurrVariation + 1);
        int catPatternVariation = Random.Range(0, maxPatternVariation + 1);

        List<int> symptomsSort = new List<int>();
        for (int i = 0; i < symptoms.Length; i++)
            if (symptoms[i])
                symptomsSort.Add(i);

        int catSymptom = Random.Range(0, symptomsSort.Count + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
