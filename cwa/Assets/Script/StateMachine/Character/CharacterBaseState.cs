using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class CharacterBaseState : BaseState<CharacterStateMachine.CharacterState>
{
    public CharacterBaseState(CharacterStateMachine.CharacterState key) : base(key)
    {
        this.StateKey = key;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override CharacterStateMachine.CharacterState GetNextState()
    {
        return this.StateKey;
    }

    public override void InitializeSubState()
    {

    }

    public override void onTriggerEnter(Collider other)
    {

    }

    public override void onTriggerExit(Collider other)
    {

    }

    public override void onTriggerStay(Collider other)
    {

    }

    public override void UpdateState()
    {

    }
}
