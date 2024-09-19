using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public enum INTERACTIONS { CatMeow, Collect, Eating, Portal, Potion, Splash, Drinking, Summon, Spell, Workbench, Branches }


public class AudioController : MonoBehaviour{

    private GameController gameController;

    public EventReference Steps;

    [Header("actions")]
    public EventReference CatMeow;
    public EventReference Collect;
    public EventReference Eating;
    public EventReference Portal;
    public EventReference Potion;
    public EventReference Splash;
    public EventReference Drinking;
    public EventReference Summon;
    public EventReference Spell;
    public EventReference Workbench;
    public EventReference Branches;

    [Header("extras")]
    public EventReference missionComple;

    // static
    private static EventReference _CatMeow;
    private static EventReference _Collect;
    private static EventReference _Eating;
    private static EventReference _Portal;
    private static EventReference _Potion;
    private static EventReference _Splash;
    private static EventReference _Drinking;
    private static EventReference _Summon;
    private static EventReference _Spell;
    private static EventReference _Workbench;
    private static EventReference _Branches;

    private static EventReference _missionComple;

    private static EventInstance potionEventInstance;

    private void Start(){
        RuntimeManager.CreateInstance(Steps).start();
        _CatMeow = CatMeow;
        _Collect = Collect;
        _Eating = Eating;
        _Portal = Portal;
        _Potion = Potion;
        _Splash = Splash;

        _Drinking = Drinking;
        _Summon = Summon;
        _Spell = Spell;
        _Workbench = Workbench;
        _Branches = Branches;

        _missionComple = missionComple;
        potionEventInstance = RuntimeManager.CreateInstance(_Potion);
    }

    public static void changeParameter(string parameter, string label){
        if(!FindObjectOfType<GameController>().isGuidedCamera)
            RuntimeManager.StudioSystem.setParameterByNameWithLabel(parameter, label);
    }

    public static void missionComplete(){
        EventInstance eventInstance = RuntimeManager.CreateInstance(_missionComple);
        eventInstance.start();
        eventInstance.release();
    }

    public static void playCraft(bool state){
        if (state)
            potionEventInstance.start();
        else
            potionEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public static void playAction(INTERACTIONS interaction){
        EventInstance eventInstance = RuntimeManager.CreateInstance(_Collect);
        switch (interaction){
            case INTERACTIONS.CatMeow:
                eventInstance = RuntimeManager.CreateInstance(_CatMeow);
                break;
            case INTERACTIONS.Collect:
                eventInstance = RuntimeManager.CreateInstance(_Collect);
                break;
            case INTERACTIONS.Eating:
                eventInstance = RuntimeManager.CreateInstance(_Eating);
                break;
            case INTERACTIONS.Portal:
                eventInstance = RuntimeManager.CreateInstance(_Portal);
                break;
            case INTERACTIONS.Potion:
                eventInstance = RuntimeManager.CreateInstance(_Potion);
                break;
            case INTERACTIONS.Splash:
                eventInstance = RuntimeManager.CreateInstance(_Splash);
                break;
            case INTERACTIONS.Drinking:
                eventInstance = RuntimeManager.CreateInstance(_Drinking);
                break;
            case INTERACTIONS.Summon:
                eventInstance = RuntimeManager.CreateInstance(_Summon);
                break;
            case INTERACTIONS.Spell:
                eventInstance = RuntimeManager.CreateInstance(_Spell);
                break;
            case INTERACTIONS.Workbench:
                eventInstance = RuntimeManager.CreateInstance(_Workbench);
                break;
            case INTERACTIONS.Branches:
                eventInstance = RuntimeManager.CreateInstance(_Branches);
                break;

            default:
                return;
        }
        GameController gameController = FindObjectOfType<GameController>();
        if (!gameController.isGuidedCamera && !gameController.dialog) { 
            eventInstance.start();
            eventInstance.release();
        }
    }
}