using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioController : MonoBehaviour{
    FMOD.Studio.EventInstance playerState;

    public FMODUnity.EventReference Steps;

    private void Start(){
        FMODUnity.RuntimeManager.CreateInstance(Steps).start();
    }

    public static void changeParameter(string parameter, string label){
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel(parameter, label);
    }
}