using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IndicatorText { COLLECT, NPC, WORKBENCH, BARRIER, CAT_MENU, CAT_AFFECTION, CAT_ANALYSE, GATE }

public class UITextIndicator : UIController
{
    public GameObject[] IndicatorsObject;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var indicator in IndicatorsObject)
            indicator?.SetActive(false);  
    }

    public void enableIndicator(IndicatorText indicator, bool state)
    {
        IndicatorsObject[(int)indicator]?.SetActive(state);
    }
}
