using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class GroundState : CharacterBaseState
{
    CharacterContext _context;
    public float MaxStableMoveSpeed = 10f;
    public float StableMovementSharpness = 15f;
    protected Vector3 _moveInputVector;
    Vector3 Gravity = new Vector3(0f,-30f,0f);

    public GroundState(CharacterContext context){
        _context = context;
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {

    }
    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        float currentVelocityMagnitude = currentVelocity.magnitude;
        Vector3 effectiveGroundNormal = _context.Motor.GroundingStatus.GroundNormal;

        // Reorient velocity on slope
        currentVelocity = this._context.Motor.GetDirectionTangentToSurface(currentVelocity, effectiveGroundNormal) * currentVelocityMagnitude;
        // Calculate target velocity
        Vector3 inputRight = Vector3.Cross(_moveInputVector, this._context.Motor.CharacterUp);
        Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * _moveInputVector.magnitude;
        Vector3 targetMovementVelocity = reorientedInput * MaxStableMoveSpeed;

        // Smooth movement Velocity
        currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-StableMovementSharpness * deltaTime));
    }

    public override void SetInputs(ref PlayerCharacterInputs inputs)
    {
        Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(inputs.MoveAxisRight, 0f, inputs.MoveAxisForward), 1f);

        // Calculate camera direction and rotation on the character plane
        Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.forward, _context.Motor.CharacterUp).normalized;
        if (cameraPlanarDirection.sqrMagnitude == 0f)
        {
            cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.up, _context.Motor.CharacterUp).normalized;
        }
        Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, _context.Motor.CharacterUp);

        _moveInputVector = cameraPlanarRotation * moveInputVector;
        if(inputs.JumpDown){
            _context._jumpRequested  = true;
        }
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }



    public override void AfterCharacterUpdate(float deltaTime)
    {

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

    public override CharacterState GetNextState()
    {
        if(_context._jumpRequested){
            return CharacterState.Jumping;
        }
        if(!_context.Motor.GroundingStatus.FoundAnyGround){
            return CharacterState.Falling;
        }
        if(_moveInputVector.sqrMagnitude > 0) {
            return CharacterState.Running;
        }
        return CharacterState.Idle;
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

    public override void InitializeSubState()
    {
    }
}
