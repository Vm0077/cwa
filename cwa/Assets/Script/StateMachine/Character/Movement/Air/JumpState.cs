using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class JumpState : AirborneState
{
    CharacterMovementContext _context;
    //Vector3 _moveInputVector;
    bool _jumpConsumed = false;
    bool _jumpedThisFrame = false;
    float JumpUpSpeed = 10f;
    float _maxJumpHeight = 3f;
    float _maxJumpTime = 0.5f;
    float[] _jumpInitialVelocity = {0, 0, 0};

    public JumpState(CharacterMovementContext context) : base(context)
    {
        _context = context;
    }

    public void SetUpJumpVariable () {
        float timeToApex =  _maxJumpTime /2;
        float _gravity = (-2 * _maxJumpHeight)/ Mathf.Pow(timeToApex,2);
        Gravity = new Vector3(0, _gravity, 0);
        _jumpInitialVelocity[0] =  (2*_maxJumpHeight) / timeToApex;
        _jumpInitialVelocity[1] =  (2*_maxJumpHeight) / timeToApex * 1.25f;
        _jumpInitialVelocity[2] =  (2*_maxJumpHeight) / timeToApex * 1.5f;
    }


    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        base.UpdateVelocity(ref currentVelocity, deltaTime);
        _jumpedThisFrame = false;
        if (_context._jumpRequested)
        {
            _context.jumpCount = ((_context.jumpCountMax + _context.jumpCount) % _context.jumpCountMax)+1;
            if(!_jumpConsumed){
                Vector3 jumpDirection = _context.Motor.CharacterUp;
                // See if we actually are allowed to jump
                // Calculate jump direction before ungrounding
                if (_context.Motor.GroundingStatus.FoundAnyGround && !_context.Motor.GroundingStatus.IsStableOnGround)
                {
                    jumpDirection = _context.Motor.GroundingStatus.GroundNormal;
                }

                // Makes the character skip ground probing/snapping on its next update.
                // If this line weren't here, the character would remain snapped to the ground when trying to jump. Try commenting this line out and see.
                _context.Motor.ForceUnground();

                currentVelocity += (jumpDirection * _jumpInitialVelocity[_context.jumpCount - 1]) - Vector3.Project(currentVelocity, _context.Motor.CharacterUp);
                _context._jumpRequested = false;
               _jumpConsumed = true;

            }
        }
        currentVelocity += Gravity * Time.deltaTime;
    }

    public override void EnterState()
    {
        SetUpJumpVariable();
        _context.animationController.animator.SetBool(_context.animationController.jumping, true);
        _context.JumpCountResetStop();
    }

    public override void ExitState()
    {
        _context.animationController.animator.SetBool(_context.animationController.jumping, false);
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
        if (_context._jumpRequested)
        {
            _context._jumpRequested = false;
        }
        if(!_jumpedThisFrame){
            _jumpConsumed = false;
        }
        _context.animationController.SetJumpCount(_context.jumpCount);
    }

    public override void BeforeCharacterUpdate(float deltaTime)
    {
    }

    public override CharacterState GetNextState()
    {
        if(_context.Motor.Velocity.y < 0){
            return CharacterState.Falling;
        }
        return CharacterState.Jumping;
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
