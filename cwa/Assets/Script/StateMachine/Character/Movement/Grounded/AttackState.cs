using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class AttackState: GroundState
{
    CharacterMovementContext _context;
    Vector3 Gravity = new Vector3(0f,-30f,0f);
    bool _isCrouching = false;
    Collider[] _probedColliders = new Collider[8];
    public float CrouchedCapsuleHeight = 1f;
    Vector3 smoothedLookInputDirection;
    float OrientationSharpness = 10f;
    bool _shouldAttack;
    int attackCount = 0;

    public AttackState(CharacterMovementContext context):base(context){
        _context = context;
    }

    public override void EnterState()
    {
        if( attackCount < 2 ) {
          attackCount ++;
        }
        _context.animationController.Attack();
        _context.animationController.SetAttactCount(attackCount);
        _shouldAttack = true;
    }


    public override void ExitState()
    {
        _context.Motor.SetCapsuleDimensions(0.5f, 2f, 1f);
        if(attackCount == 2)  attackCount  = 0;
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
    }

    public override void UpdateState()
    {
        if(!_context.animationController.animator.GetCurrentAnimatorStateInfo(0).IsName("attack1")){
            _shouldAttack = false;
            Debug.Log(_shouldAttack);
        }

    }
    public override void AfterCharacterUpdate(float deltaTime)
    {
      //if(Physics.BoxCast())
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
        if(_shouldAttack) {
            return CharacterState.Attack;
        }
        return CharacterState.Idle;
    }
    public override void InitializeSubState()
    {

    }

}
