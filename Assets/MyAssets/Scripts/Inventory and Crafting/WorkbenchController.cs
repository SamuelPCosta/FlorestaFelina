using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchController : MonoBehaviour
{
    public GameObject craftingMenu;
    // Start is called before the first frame update
    void Start()
    {
        craftingMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //PRIVATES 

    //PUBLICS
    public void turnOnMenu()
    {
        craftingMenu.SetActive(true);
    }

    public void turnOffMenu()
    {
        craftingMenu.SetActive(false);
    }
}
