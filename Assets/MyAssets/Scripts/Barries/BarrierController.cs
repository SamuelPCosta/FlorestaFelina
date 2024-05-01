using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public GameObject orb;

    private void Start()
    {
        orb.SetActive(false);
    }

    //TOPO: conferir metodo de liberar caminho e remover ou trazer de volta
    void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.tag == "MakeWay")
        //    //Chama animacao
        //    destroy();
    }

    public void makeWay()
    {
        //TODO pegar animator e ativar animacao de abrir caminho
        orb.SetActive(true);
        Invoke("destroy", 2f);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
