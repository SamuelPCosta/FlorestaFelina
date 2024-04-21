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
    }

    [SerializeField] public Speech[] speeches;

    public Speech[] getSpeeches()
    {
        return speeches;
    }

    public void markTutorial()
    {
        FindObjectOfType<TutorialSave>().saveTutorial(gameObject);
        gameObject.SetActive(false);
    }
}