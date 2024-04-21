using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public bool[] tutorialsLvl1 = new bool[5];

    public int water;
    public int plant1;
    public int plant2;
    public int potion1;
    public int potion2;
    public int potion3;

    //Salvar n�vel(E PORTAL ANTERIOR)
    //Salvar posicao, X, Y, Z
    //Se est� com gato nas costas
    //    Salva atributos do gato
    //Salvar os 5 itens do invent�rio
    //Salvar miss�o atual
    //Salvar estados das plantas coletadas
    //Salvar portais usados
    //Salvar barreiras destruidas
}
