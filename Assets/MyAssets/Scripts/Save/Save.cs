using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum levels {LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5}

[System.Serializable]
public class Save
{
    public bool[] dialogsLvl1 = new bool[5];
    public bool[] dialogsLvl2 = new bool[5];
    public bool[] dialogsLvl3 = new bool[5];
    public bool[] dialogsLvl4 = new bool[5];
    public bool[] dialogsLvl5 = new bool[5];

    public int water = 0;
    public int plant1 = 0;
    public int plant2 = 0;
    public int potion1 = 0;
    public int potion2 = 0;
    public int potion3 = 0;

    //Salvar nível(E PORTAL ANTERIOR)
    //Salvar posicao, X, Y, Z
    //Se está com gato nas costas
    //    Salva atributos do gato
    //Salvar missão atual
    //Salvar estados das plantas coletadas
    //Salvar portais usados
    //Salvar barreiras destruidas
}
