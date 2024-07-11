using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Speeches : MonoBehaviour
{
    public enum Character { PLAYER, NPC1, NPC2 }

    [Serializable]
    public class Speech
    {
        [SerializeField] public Character character = new Character();
        public string speech;
        public string speechInEnglish;
    }

    [SerializeField] public Speech[] speeches;

    public Speech[] getSpeeches()
    {
        return speeches;
    }

    public void activeDialog(bool state)
    {
        GetComponent<BoxCollider>().enabled = state;
    }

    public void markDialog()
    {
        FindObjectOfType<DialogSave>().saveDialog(gameObject);
        activeDialog(false);
    }
}