using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State <T> where T : Enum{

    protected State<T> _currentSuperState;
    protected State<T> _currentSubState;

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract T GetNextState();
    public abstract void onTriggerEnter(Collider other);
    public abstract void onTriggerStay(Collider other);
    public abstract void onTriggerExit(Collider other);

    public abstract void InitializeSubState();
    void UpdateStates() {
        UpdateState();
        if(_currentSubState != null){
            _currentSubState.UpdateStates();
        }
    }
    protected void SetSuperState(State<T> newSuperState){
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(State<T> newSubState){
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
};
