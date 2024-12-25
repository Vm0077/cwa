using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasState {
    TitleMenu,
    OptionMenu,
    GameSaveMenu,
}
public class MenuCanvasStateMachine : MonoBehaviour, IStateMachine
{
    [SerializeField] public Canvas gameSaveCanvas;
    [SerializeField] public Canvas optionCanvas;
    [SerializeField] public Canvas menuCanvas;

    Dictionary <CanvasState, StartMenuState> states  = new Dictionary<CanvasState,StartMenuState>();
    StartMenuState CurrentState;
    CanvasState currentKey;
    CanvasState  previousKey;
    bool isTransitioning = false;

    void InitializeState(){
        states.Add(CanvasState.TitleMenu, new TitleMenuState(menuCanvas));
        states.Add(CanvasState.OptionMenu, new OptionState(optionCanvas));
        states.Add(CanvasState.GameSaveMenu, new GameSaveState(gameSaveCanvas));
        CurrentState = states[CanvasState.TitleMenu];
        currentKey = CanvasState.TitleMenu;
        previousKey = CanvasState.TitleMenu;
    }


    public void TransitionToState(CanvasState key){
       isTransitioning = true;
       CurrentState.ExitState();
       CurrentState = states[key];
       CurrentState.EnterState();
       isTransitioning = false;
    }

    public void UpdateState(){
        previousKey = currentKey;
        currentKey = CurrentState.GetNextState();
        if(isTransitioning) return;
        if(!currentKey.Equals(previousKey)){
            TransitionToState(currentKey);
        }
    }
    void Awake() {
        InitializeState();
    }
    void Update() {
        UpdateState();
        CurrentState.UpdateState();
        Debug.Log(currentKey);
    }

    public void GotoGameSave(){
        currentKey = CanvasState.GameSaveMenu;
        TransitionToState(currentKey);
    }
    public void GotoOptionState(){
        currentKey = CanvasState.OptionMenu;
        Debug.Log("nah");
        TransitionToState(currentKey);
    }
    public void GotoTitle(){
        currentKey = CanvasState.TitleMenu;
        TransitionToState(currentKey);
    }
}
