using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtons : UIController
{
    private GameObject btnCurrent;
    private Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        btnCurrent = null;
        buttons = null;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;
        if ((btnSelected == null || !btnSelected.GetComponent<Button>().interactable) && btnCurrent != null)
        {
            btnSelected = btnCurrent;
            EventSystem.current.SetSelectedGameObject(btnSelected);
        }

        if (btnSelected != null && btnSelected.GetComponent<Button>().interactable)
        {
            btnCurrent = btnSelected;
            selectOption(btnSelected);
        }

        print(EventSystem.current.currentSelectedGameObject);
    }

    public void setButtons(Button[] buttons){
        this.buttons = buttons;
        //foreach (Button button in buttons)
        //    print(button);
    }

    public void setOptions(GameObject button, bool condition){   
        if (condition)
            button.GetComponent<Button>().interactable = true;
        else
            button.GetComponent<Button>().interactable = false;
    }

    public void disableOptions()
    {
        TextMeshProUGUI[] texts;
        foreach (Button button in buttons)
        {
            //Altera cor dos textos
            texts = button.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in texts)
                text.color = ColorPalette.disableText;

            //Desliga indicadores
            getIndicator(button.gameObject).SetActive(false);
        }
    }

    public void enableOption(int index)
    {
        //Altera cor dos textos
        TextMeshProUGUI[] texts = buttons[index].GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
            text.color = ColorPalette.enableText;
    }

    private void selectOption(GameObject btnSelected)
    {
        foreach (Button button in buttons)
            getIndicator(button.gameObject).SetActive(false);
        getIndicator(btnSelected).SetActive(true);
    }

    private GameObject getIndicator(GameObject button)
    {
        return button.transform.GetChild(0).gameObject;
    }
}
