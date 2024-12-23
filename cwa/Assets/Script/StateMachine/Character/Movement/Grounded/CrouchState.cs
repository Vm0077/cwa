using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class CrouchState: GroundState
{
    CharacterMovementContext _context;
    Vector3 Gravity = new Vector3(0f,-30f,0f);
    bool _isCrouching = false;
    Collider[] _probedColliders = new Collider[8];
    public float CrouchedCapsuleHeight = 1f;
    Vector3 smoothedLookInputDirection;
    float OrientationSharpness = 10f;
    bool _shouldCrouch;

    public CrouchState(CharacterMovementContext context):base(context){
        _context = context;
    }

    public override void EnterState()
    {
        _isCrouching = true;
        _shouldCrouch = true;
        _context.animationController.animator.SetBool(_context.animationController.crouch, true);
        _context.Motor.SetCapsuleDimensions(0.5f, CrouchedCapsuleHeight, CrouchedCapsuleHeight * 0.3f);
         // If obstructions, just stick to crouching dimensions
    }


    public override void ExitState()
    {
        _context.Motor.SetCapsuleDimensions(0.5f, 2f, 1f);
        _context.animationController.animator.SetBool(_context.animationController.crouch, false);
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        if (_context._lookInputVector.sqrMagnitude > 0f && OrientationSharpness > 0f)
        {
            // Smoothly interpolate from current to target look direction
            smoothedLookInputDirection = Vector3.Slerp(_context.Motor.CharacterForward, _context._lookInputVector, 1 - Mathf.Exp(-OrientationSharpness * deltaTime)).normalized;
            // Set the current rotation (which will be used by the KinematicCharacterMotor)
            currentRotation = Quaternion.LookRotation(smoothedLookInputDirection, _context.Motor.CharacterUp);
        }
    }


    public override void SetInputs(ref PlayerCharacterInputs inputs)
    {
            base.SetInputs(ref inputs);
            if(_moveInputVector.sqrMagnitude > 0) {
                _context.animationController.animator.SetBool(_context.animationController.running,true);
            }else {
                _context.animationController.animator.SetBool(_context.animationController.running,false);
            }
            if(inputs.CrouchDown){
                Debug.Log("down");
                _shouldCrouch = true;
                if(_isCrouching) return;
                _isCrouching = true;
            }else if(inputs.CrouchUp) {
                _shouldCrouch = false;
                Debug.Log("up");
            }
    }

    public override void UpdateState()
    {

    }
    public override void AfterCharacterUpdate(float deltaTime)
    {
        // Do an overlap test with the character's standing height to see if there are any obstructions
        if(_shouldCrouch) return;
            _context.Motor.SetCapsuleDimensions(0.5f, 2f, 1f);
        if (_context.Motor.CharacterOverlap(
            _context.Motor.TransientPosition,
            _context.Motor.TransientRotation,
            _probedColliders,
            _context.Motor.CollidableLayers,
            QueryTriggerInteraction.Ignore) > 0)
        {
            // If obstructions, just stick to crouching dimensions
            _context.Motor.SetCapsuleDimensions(0.5f, CrouchedCapsuleHeight, CrouchedCapsuleHeight * 0.3f);
            Debug.Log("sealing");
        }
        else
        {
            // If no obstructions, uncrouch
            //MeshRoot.localScale = new Vector3(1f, 1f, 1f);
            _isCrouching = false;
        }
    }

    public override void BeforeCharacterUpdate(float deltaTime)
    {

    }
    public override bool IsColliderValidForCollisions(Collider coll)
    {
      return true;
    }

    public override void OnDiscreteCollisionDetected(Collider hitCollider)
    {

    }

    public override void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {

    }

    public override void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {

    }

    public override void PostGroundingUpdate(float deltaTime)
    {

    }

    public override void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {

    }


    public override void onTriggerEnter(Collider other)
    {

    }

    public override void onTriggerStay(Collider other)
    {
    }

    public override void onTriggerExit(Collider other)
    {

    }

    public override CharacterState GetNextState()
    {
        if(!_context.Motor.GroundingStatus.FoundAnyGround){
            return CharacterState.Falling;
        }
        if(!_shouldCrouch && !_isCrouching){
            return CharacterState.Idle;
        }
        return CharacterState.Crouching;
    }
    public override void InitializeSubState()
    {

    }

}
