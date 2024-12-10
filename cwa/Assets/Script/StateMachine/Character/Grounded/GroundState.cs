using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class GroundState : CharacterBaseState, ICharacterController
{
    public GroundState(CharacterStateMachine.CharacterState key) : base(key)
    {
        this.StateKey = key;
    }
    public override void ExitState(){}
    public override  void EnterState(){}
    public override  void UpdateState(){
    }
    public override  CharacterStateMachine.CharacterState GetNextState(){
     return this.StateKey;
    }
    public override  void onTriggerEnter(Collider other){}
    public override  void onTriggerStay(Collider other){}
    public override  void onTriggerExit(Collider other){}

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
    }

    public void PostGroundingUpdate(float deltaTime)
    {
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
    }
}
