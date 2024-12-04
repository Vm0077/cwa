using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class BaseState <EState> where EState : Enum {
    public BaseState(EState key) {
        StateKey = key;
    }
    public EState StateKey;
    protected BaseState<EState> _currentSuperState;
    protected BaseState<EState> _currentSubState;

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();
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

    protected void SetSuperState(BaseState<EState> newSuperState){
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(BaseState<EState> newSubState){
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }


};
