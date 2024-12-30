using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class AirborneState : CharacterMovementBaseState
{
    CharacterMovementContext _context;
    public float MaxStableMoveSpeed = 20f;
    public float StableMovementSharpness = 15f;
    protected Vector3 _moveInputVector;
    [Header("Air Movement")]
    public float MaxAirMoveSpeed = 15f;
    public float AirAccelerationSpeed = 15f;
    public float Drag = 0.1f;
    public Vector3 Gravity = new Vector3(0, -30f, 0);

    public AirborneState(CharacterMovementContext context)
    {
        _context = context;
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {

    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        if (_moveInputVector.sqrMagnitude > 0f)
        {
            Vector3 addedVelocity = _moveInputVector * AirAccelerationSpeed * deltaTime;

            Vector3 currentVelocityOnInputsPlane = Vector3.ProjectOnPlane(currentVelocity, _context.Motor.CharacterUp);

            // Limit air velocity from inputs
            if (currentVelocityOnInputsPlane.magnitude < MaxAirMoveSpeed)
            {
                // clamp addedVel to make total vel not exceed max vel on inputs plane
                Vector3 newTotal = Vector3.ClampMagnitude(currentVelocityOnInputsPlane + addedVelocity, MaxAirMoveSpeed);
                addedVelocity = newTotal - currentVelocityOnInputsPlane;
            }
            else
            {
                // Make sure added vel doesn't go in the direction of the already-exceeding velocity
                if (Vector3.Dot(currentVelocityOnInputsPlane, addedVelocity) > 0f)
                {
                    addedVelocity = Vector3.ProjectOnPlane(addedVelocity, currentVelocityOnInputsPlane.normalized);
                }
            }

            // Prevent air-climbing sloped walls
            if (_context.Motor.GroundingStatus.FoundAnyGround)
            {
                if (Vector3.Dot(currentVelocity + addedVelocity, addedVelocity) > 0f)
                {
                    Vector3 perpenticularObstructionNormal = Vector3.Cross(Vector3.Cross(_context.Motor.CharacterUp, _context.Motor.GroundingStatus.GroundNormal), _context.Motor.CharacterUp).normalized;
                    addedVelocity = Vector3.ProjectOnPlane(addedVelocity, perpenticularObstructionNormal);
                }
            }
            currentVelocity += addedVelocity;
        }
    }

    public override void SetInputs(ref PlayerCharacterInputs inputs)
    {
        if(!_context.allowToMove){
            return;
        }
        Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(inputs.MoveAxisRight, 0f, inputs.MoveAxisForward), 1f);

        // Calculate camera direction and rotation on the character plane
        Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.forward, _context.Motor.CharacterUp).normalized;
        if (cameraPlanarDirection.sqrMagnitude == 0f)
        {
            cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.up, _context.Motor.CharacterUp).normalized;
        }
        Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, _context.Motor.CharacterUp);

        _moveInputVector = cameraPlanarRotation * moveInputVector;
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
           return CharacterState.Idle;
    }
}
