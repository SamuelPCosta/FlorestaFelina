using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum levels {LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5}

[System.Serializable]
public class Save
{
    //CONSTS
    public static int numberOfLevels = 5;
    public static int numberOfCats = 20;


    //PLAYER
    public int level = 0;
    public float[] playerPosition = new float[3];
    public float[] playerPositionPortal = new float[3];
    public int orientation = 0;
    public int previousLevel = -1;
    public int journal = 0;
    public bool isBanana = false;
    public bool step = false;


    //CATS AND MISSIONS
    public int currentMission = -1;
    public int currentMissionStage = -1;
    public MISSION_STATE[] missionState = new MISSION_STATE[numberOfCats];

    public string[] catsNames = new string[numberOfCats];
    public int[] numColors = new int[numberOfCats];
    public int[] numColorVariation = new int[numberOfCats];
    public int[] eyesVariation = new int[numberOfCats];


    //ITEMS
    public int water = 0;
    public int fish = 0;
    public int plant1 = 0;
    public int plant2 = 0;
    public int potion1 = 0;
    public int potion2 = 0;
    public int potion3 = 0;


    //DIALOGS
    public bool[,] dialogs = new bool[numberOfLevels, 8];

    public bool objectiveFloorDestroy;

    //Salvar estados das plantas coletadas
}
