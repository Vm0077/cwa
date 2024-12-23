using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class RunState : GroundState
{
    CharacterMovementContext _context;
    Vector3 Gravity = new Vector3(0f, -30f, 0f);
    Vector3 smoothedLookInputDirection;
    float OrientationSharpness = 10f;
    public RunState(CharacterMovementContext context) : base(context)
    {
        _context = context;
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
        _context._lookInputVector = _moveInputVector.normalized;
    }

    public override void EnterState()
    {
        base.EnterState();
        _context.animationController.animator.speed = 2.0f;
        _context.animationController.animator.SetBool(_context.animationController.running, true);
    }

    public override void ExitState()
    {
        base.ExitState();

    }

    public override void UpdateState()
    {
        // check if the vector on the right or left side;
        Vector3 dir = Vector3.Cross(_context.Motor.CharacterForward,_context._lookInputVector);
        _context.animationController.SetRunVelocity(new Vector3(dir.y,0,1));
    }


}
