using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum levels {LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5}

[System.Serializable]
public class Save
{
    public float playerX = 0;
    public float playerY = 0;
    public float playerZ = 0;

    public int level = 0;

    public int water = 0;
    public int plant1 = 0;
    public int plant2 = 0;
    public int potion1 = 0;
    public int potion2 = 0;
    public int potion3 = 0;

    public bool[] dialogsLvl1 = new bool[5];
    public bool[] dialogsLvl2 = new bool[5];
    public bool[] dialogsLvl3 = new bool[5];
    public bool[] dialogsLvl4 = new bool[5];
    public bool[] dialogsLvl5 = new bool[5];

    //Salvar n�vel(E PORTAL ANTERIOR)
    //Se est� com gato nas costas
    //    Salva atributos do gato
    //Salvar miss�o atual
    //Salvar estados das plantas coletadas
    //Salvar portais usados
    //Salvar barreiras destruidas
}
