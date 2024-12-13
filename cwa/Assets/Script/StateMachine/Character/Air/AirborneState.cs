using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class AirborneState : CharacterBaseState
{
    CharacterContext _context;
    public float MaxStableMoveSpeed = 10f;
    public float StableMovementSharpness = 15f;
    Vector3 _moveInputVector;
    Vector3 Gravity = new Vector3(0, -30f, 0);

    public AirborneState(CharacterContext context)
    {
        _context = context;
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {

    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity += Gravity * deltaTime;
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
        _moveInputVector = moveInputVector;
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
        if(_context.Motor.GroundingStatus.IsStableOnGround){
            return CharacterState.Idle;
        }
        if(_context.Motor.Velocity.y > 0 && _context.isJumpPressed){
          return CharacterState.Jumping;
        }
        return CharacterState.Falling;
    }
}
