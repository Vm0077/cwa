using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public enum CharacterState {
    // Ground
   Idle,
   Running,
   Crouching,
    // Airborne,
   Jumping,
   Falling,
}
public class CharacterStateMachine :MonoBehaviour, IStateMachine
{
    Dictionary <CharacterState, CharacterBaseState> states  = new Dictionary<CharacterState,CharacterBaseState> ();
    CharacterContext _context;
    CharacterBaseState CurrentState;
    CharacterState currentKey;
    CharacterState previousKey;
    public KinematicCharacterMotor motor;
    public Animator animator;
    bool isTransitioning = false;

    void InitializeState(){
        states.Add(CharacterState.Idle, new IdleState(_context));
        states.Add(CharacterState.Running, new RunState(_context));
        states.Add(CharacterState.Crouching, new CrouchState(_context));

        states.Add(CharacterState.Jumping, new JumpState(_context));
        states.Add(CharacterState.Falling, new FallState(_context));
        CurrentState = states[CharacterState.Idle];
        currentKey = CharacterState.Idle;
        previousKey = CharacterState.Idle;
    }

    public void TransitionToState(CharacterState key){
       isTransitioning = true;
       CurrentState.ExitState();
       CurrentState = states[key];
       CurrentState.EnterState();
       _context.Motor.CharacterController = CurrentState;
       isTransitioning = false;
    }

    public void UpdateState(){
        previousKey = currentKey;
        currentKey = CurrentState.GetNextState();
        Debug.Log(currentKey);
        if(isTransitioning) return;
        if(!currentKey.Equals(previousKey)){
            TransitionToState(currentKey);
        }
    }
    void Awake() {
        _context = new CharacterContext(motor, animator);
        InitializeState();
        motor.CharacterController = states[CharacterState.Idle];
    }
    void Update() {
        UpdateState();
    }
    public void SetInputs(ref PlayerCharacterInputs inputs){
        if(CurrentState == null) Debug.Log("it is null");
        CurrentState.SetInputs(ref inputs);
    }
}

