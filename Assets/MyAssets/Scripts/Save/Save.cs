using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum levels {LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5}

[System.Serializable]
public class Save
{
    public static int numberOfLevels = 5;

    public float playerX = 0;
    public float playerY = 0;
    public float playerZ = 0;

    public int level = 0;

    public int currentMission = -1;

    public int water = 0;
    public int plant1 = 0;
    public int plant2 = 0;
    public int potion1 = 0;
    public int potion2 = 0;
    public int potion3 = 0;

    public bool[,] dialogs = new bool[numberOfLevels, 6];

    public bool objectiveFloorDestroy;

    //Salvar nível(E PORTAL ANTERIOR)
    //Se está com gato nas costas
    //    Salva atributos do gato
    //Salvar missão atual
    //Salvar estados das plantas coletadas
    //Salvar portais usados
    //Salvar barreiras destruidas
}
