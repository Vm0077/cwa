using System.Collections.Generic;
using System;
using UnityEngine;

public class StateMachine <EState> : MonoBehaviour where EState: Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState>  CurrrentStates ;
    protected bool isTrasitioningState;

    void Start()
    {
        CurrrentStates.EnterState();
    }

    void Update()
    {
        EState nextStateKey =  CurrrentStates.GetNextState();
        if(!isTrasitioningState &&  nextStateKey.Equals(CurrrentStates.StateKey)){
            CurrrentStates.UpdateState();
        } else if (!isTrasitioningState) {
            TransitionToState(nextStateKey);
        }
    }

    public void TransitionToState(EState stateKey)
    {
        isTrasitioningState = true;
        CurrrentStates.ExitState();
        CurrrentStates = States[stateKey];
        CurrrentStates.EnterState();
        isTrasitioningState = false;
    }

    void onTriggerEnter(Collider other)
    {
        CurrrentStates.onTriggerEnter(other);
    }
    void onTriggerStay(Collider other)
    {
        CurrrentStates.onTriggerStay(other);
    }
    void onTriggerExit(Collider other){
        CurrrentStates.onTriggerExit(other);
    }

}
