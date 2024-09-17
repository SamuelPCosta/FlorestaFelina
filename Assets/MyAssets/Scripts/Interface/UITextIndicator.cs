using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IndicatorText { COLLECT, NPC, WORKBENCH, BARRIER, CAT_MENU, CAT_AFFECTION, CAT_ANALYSE, CAT_BAG, GATE, SUMMON, PORTAL, FISH }

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
        if (state && indicator != IndicatorText.CAT_ANALYSE && indicator != IndicatorText.CAT_AFFECTION)
            foreach (var indicatorObj in IndicatorsObject)
                indicatorObj?.SetActive(false);
        IndicatorsObject[(int)indicator]?.SetActive(state);
    }
}
