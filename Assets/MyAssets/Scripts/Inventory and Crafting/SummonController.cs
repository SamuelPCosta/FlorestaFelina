using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonController : MonoBehaviour
{
    public GameObject workbench;
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        workbench.SetActive(false);
        portal.SetActive(false);
    }

    public void summonStructures()
    {
        //TODO: controlar efeitos visuais, cameras etc da transicao
        workbench.SetActive(true);
        portal.SetActive(true);
        gameObject.SetActive(false);
    }
}
