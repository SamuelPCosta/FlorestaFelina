using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarrier : MonoBehaviour
{
    public GameObject indicator;

    void Start()
    {
        indicator.SetActive(false);
    }
    public void spawnTextIndicator(bool state)
    {
        if(state)
            indicator.SetActive(true);
        else
            indicator.SetActive(false);
    }
}
