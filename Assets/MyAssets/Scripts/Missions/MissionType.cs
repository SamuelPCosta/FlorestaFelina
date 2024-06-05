using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MissionType : MonoBehaviour
{
    [SerializeField] public Mission mission = new Mission();
    public string title;
    public string[] description = new string[3];
}
