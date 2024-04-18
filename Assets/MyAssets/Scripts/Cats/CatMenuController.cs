using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CatMenuController : MonoBehaviour
{
    [Header("Buttons")]
    public Button[] buttons;

    [Header("Attributes")]
    public GameObject catMenu;

    // Start is called before the first frame update
    void Start()
    {
        catMenu.SetActive(false);
    }

    public void turnOn()
    {
        catMenu?.SetActive(true);
    }

    public void turnOff()
    {
        catMenu?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
