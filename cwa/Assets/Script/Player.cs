using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using KinematicCharacterController;
public struct PlayerCharacterInputs {
  public float MoveAxisForward;
  public float MoveAxisRight;
  public Quaternion CameraRotation;
  public bool JumpDown;
  public bool CrouchDown;
  public bool CrouchUp;
}

public class Player : MonoBehaviour {
  public CharacterStateMachine Character;
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
    PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();
    // Build the CharacterInputs struct
    characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
    characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
    characterInputs.CameraRotation = Camera.main.transform.rotation;
    characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
    characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
    characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);
    Character.SetInputs(ref characterInputs);
  }
}
