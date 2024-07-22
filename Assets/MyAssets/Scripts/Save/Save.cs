using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum levels {LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5}

[System.Serializable]
public class Save
{
    //CONSTS
    public static int numberOfLevels = 5;
    public static int numberOfCats = 20;


    //PLAYER
    public float[] playerPosition = new float[3];
    public int orientation = 0;
    public int level = 0;
    public int previousLevel = -1;


    //CATS AND MISSIONS
    public int currentMission = -1;
    public int currentMissionStage = -1;
    public MISSION_STATE[] missionState = new MISSION_STATE[numberOfCats];

    public string[] catsNames = new string[numberOfCats];
    public int[] catsVariations = new int[numberOfCats];
    public float[,] catsPosition = new float[numberOfCats, 3];


    //ITEMS
    public int water = 0;
    public int plant1 = 0;
    public int plant2 = 0;
    public int potion1 = 0;
    public int potion2 = 0;
    public int potion3 = 0;


    //DIALOGS
    public bool[,] dialogs = new bool[numberOfLevels, 8];

    public bool objectiveFloorDestroy;

    //Salvar nível(E PORTAL ANTERIOR)
    //Salvar estados das plantas coletadas
    //Salvar portais usados
    //Salvar barreiras destruidas
}
