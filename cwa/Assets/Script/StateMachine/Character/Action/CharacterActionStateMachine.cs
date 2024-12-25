using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionStateMachine : MonoBehaviour,IStateMachine
{
   // Dictionary <CharacterState, CharacterMovementBaseState> states  = new Dictionary<CharacterState,CharacterMovementBaseState>();
   // public CharacterMovementContext _context;
   // CharacterMovementBaseState CurrentState;
   // CharacterState currentKey;
   // CharacterState previousKey;
   // [SerializeField] public TMPro.TextMeshProUGUI text;
   // public CharacterAnimationController AnimationController;
   // public Animator animator;
   // bool isTransitioning = false;

   // void InitializeState(){
   //     states.Add(CharacterState.Idle, new IdleState(_context));
   //     states.Add(CharacterState.Running, new RunState(_context));
   //     states.Add(CharacterState.Crouching, new CrouchState(_context));
   //     states.Add(CharacterState.Jumping, new JumpState(_context));
   //     states.Add(CharacterState.Falling, new FallState(_context));
   //     CurrentState = states[CharacterState.Idle];
   //     currentKey = CharacterState.Idle;
   //     previousKey = CharacterState.Idle;
   // }


   // public void TransitionToState(CharacterState key){
   //    isTransitioning = true;
   //    CurrentState.ExitState();
   //    CurrentState = states[key];
   //    CurrentState.EnterState();
   //    _context.Motor.CharacterController = CurrentState;
   //    isTransitioning = false;
   // }

   // public void UpdateState(){
   //     previousKey = currentKey;
   //     currentKey = CurrentState.GetNextState();
   //     if(isTransitioning) return;
   //     if(!currentKey.Equals(previousKey)){
   //         text.text = currentKey.ToString();
   //         TransitionToState(currentKey);
   //     }
   // }
   // void Awake() {
   //     _context.animationController = AnimationController;
   //     InitializeState();
   // }
   // void Update() {
   //     UpdateState();
   //     CurrentState.UpdateState();
   // }
   // public void SetInputs(ref PlayerCharacterInputs inputs){
   //     CurrentState.SetInputs(ref inputs);
   // }

   // public void  AssignStartCoroutine(IEnumerator routine){
   //         StartCoroutine(routine);
   // }
   // void Start()
   // {

   // }
}
