using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public GameObject orb;

    private void Start(){
        orb.SetActive(false);
    }

    public void makeWay()
    {
        //TODO pegar animator e ativar animacao de abrir caminho
        orb.SetActive(true);
        AudioController.playAction(INTERACTIONS.Spell);
        Invoke("destroy", 2f);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
