using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICrafting : UIController
{
    [Header("Values  - potion1")]
    public GameObject option1item1;
    public GameObject option1item2;

    public GameObject option2item1;
    public GameObject option2item2;

    public GameObject option3item1;
    public GameObject option3item2;


    private enum InventoryItem { PLANT1, PLANT2, WATER, POTION1, POTION2 }

    public void refreshWorkbench(int[] inventory, int[] craft1, int[] craft2, int[] craft3)
    {
        option1item1.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.PLANT1] + "/" + craft1[0];
        option1item2.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.WATER] + "/" + craft1[1];

        option2item1.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.PLANT2] + "/" + craft2[0];
        option2item2.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.WATER] + "/" + craft2[1];

        option3item1.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.POTION1] + "/" + craft3[0];
        option3item2.GetComponent<TextMeshProUGUI>().text = inventory[(int)InventoryItem.POTION2] + "/" + craft3[1];
    }
}
