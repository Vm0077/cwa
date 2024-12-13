using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class FallState :AirborneState
{

    CharacterContext _context;
    public float MaxStableMoveSpeed = 10f;
    public float StableMovementSharpness = 15f;
    Vector3 _moveInputVector;
    Vector3 Gravity = new Vector3(0, -30f, 0);

    public FallState(CharacterContext context):base(context)
    {
        _context = context;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity += Gravity * deltaTime;
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
}
