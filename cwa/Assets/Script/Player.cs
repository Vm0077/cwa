using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using KinematicCharacterController;
public struct PlayerCharacterInputs {
  public float MoveAxisForward;
  public float MoveAxisRight;
  public Quaternion CameraRotation;
  public bool AttackPressed;
  public bool JumpDown;
  public bool CrouchDown;
  public bool CrouchUp;
}

public class Player : MonoBehaviour {
  public CharacterMovementStateMachine CharacterMovement;
  public CharacterAnimationController AnimationController;
  private const string MouseXInput = "Mouse X";
  private const string MouseYInput = "Mouse Y";
  private const string MouseScrollInput = "Mouse ScrollWheel";
  private const string HorizontalInput = "Horizontal";
  private const string VerticalInput = "Vertical";

  private void Start() {
  }

  private void Update() {
    HandleCharacterInput();
  }

  private void LateUpdate() {
     HandleCameraInput();
  }
  private void HandleCameraInput()
  {
    //Input(Time.deltaTime, scrollInput, lookInputVector);
  }

  private void HandleCharacterInput() {
    CharacterMovement._context.inputs = new PlayerCharacterInputs();
    CharacterMovement._context.inputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
    CharacterMovement._context.inputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
    CharacterMovement._context.inputs.CameraRotation = Camera.main.transform.rotation;
    CharacterMovement._context.inputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
    CharacterMovement._context.inputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
    CharacterMovement._context.inputs.CrouchUp = Input.GetKeyUp(KeyCode.C);
    CharacterMovement._context.inputs.AttackPressed = Input.GetMouseButtonDown(0);
    CharacterMovement.SetInputs(ref  CharacterMovement._context.inputs);
  }
}
