using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string nameOfPotion;
    protected PotionType type;

    private static string potion1 = "Pocao 1";
    private static string potion2 = "Pocao 2";
    private static string potion3 = "Pocao 3";

    public static string getPotionName(PotionType type)
    {
        switch (type) {
            case PotionType.POTION1: 
                return potion1;
            case PotionType.POTION2:
                return potion2;
            case PotionType.POTION3:
                return potion3;
            default:
                return "";
        }
    }
}

