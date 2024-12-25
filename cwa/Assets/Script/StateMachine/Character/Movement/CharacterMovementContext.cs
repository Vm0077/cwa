using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class CharacterMovementContext : MonoBehaviour
{
    public KinematicCharacterMotor Motor;
    public bool _jumpRequested = false;
    public Vector3 currentVelocity;
    public int jumpCount = 0;
    public int jumpCountMax = 3;

    public Vector3 _lookInputVector;
    public PlayerCharacterInputs inputs;
    public CharacterAnimationController animationController;
    Coroutine jumpCountRoutine = null;

    public CharacterMovementContext(KinematicCharacterMotor motor, CharacterAnimationController characterAnimation)
    {
        this.Motor = motor;
        this.animationController = characterAnimation;
    }
    IEnumerator  resetJumpCount(){
        yield return new WaitForSeconds(0.1f);
        jumpCount = 0;
    }

    public void JumpCountResetStart(){
        jumpCountRoutine = StartCoroutine(resetJumpCount());
    }
    public void JumpCountResetStop(){
        if( jumpCount < jumpCountMax && jumpCountRoutine != null){
            Debug.Log("stop it");
            StopCoroutine(jumpCountRoutine);
        }
    }

    void Awake () {
      jumpCountMax = 3;
    }

}
