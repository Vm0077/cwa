using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class FallState :AirborneState
{

    CharacterMovementContext _context;
    Vector3 _jumpToFallGravity = new Vector3(0,-30f,0);

    public FallState(CharacterMovementContext context):base(context)
    {
        _context = context;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        base.UpdateVelocity(ref currentVelocity, deltaTime);
        currentVelocity += Gravity * deltaTime;
    }

    public override void EnterState()
    {
        Gravity = _jumpToFallGravity;
        _context.animationController.animator.SetBool(_context.animationController.falling, true);
    }


    public override void ExitState()
    {
        base.ExitState();
        _context.animationController.animator.SetBool(_context.animationController.falling, false);
        _context.JumpCountResetStart();
    }

    public override void UpdateState()
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

    public override void InitializeSubState()
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
        if(_context.Motor.GroundingStatus.FoundAnyGround){
            if(_moveInputVector.sqrMagnitude > 0){
                Debug.Log("nice");
                return CharacterState.Running;
            }
            return CharacterState.Idle;
        }
        return CharacterState.Falling;
    }
}
