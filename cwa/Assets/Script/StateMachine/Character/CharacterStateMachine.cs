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
    Dictionary <CharacterState, CharacterBaseState> states  = new Dictionary<CharacterState,CharacterBaseState>();
    public CharacterContext _context;
    CharacterBaseState CurrentState;
    CharacterState currentKey;
    CharacterState previousKey;
    public KinematicCharacterMotor motor;
    [SerializeField] public TMPro.TextMeshProUGUI text;
    public CharacterAnimationController AnimationController;
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
        if(isTransitioning) return;
        if(!currentKey.Equals(previousKey)){
            text.text = currentKey.ToString();
            TransitionToState(currentKey);
        }
    }
    void Awake() {
        _context.Motor = motor;
        _context.animationController = AnimationController;
        InitializeState();
        motor.CharacterController = states[CharacterState.Idle];
    }
    void Update() {
        UpdateState();
        CurrentState.UpdateState();
    }
    public void SetInputs(ref PlayerCharacterInputs inputs){
        CurrentState.SetInputs(ref inputs);
    }

    public void  AssignStartCoroutine(IEnumerator routine){
            StartCoroutine(routine);
    }

}

