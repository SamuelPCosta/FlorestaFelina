using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "MakeWay")
            //Chama animacao
            destroy();
    }

    public void makeWay()
    {
        //TODO pegar animator e ativar animacao de abrir caminho
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
