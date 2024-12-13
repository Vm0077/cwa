using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class RunState : GroundState
{
    CharacterContext _context;
    Vector3 Gravity = new Vector3(0f, -30f, 0f);

    public RunState(CharacterContext context) : base(context)
    {
        _context = context;
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
    // if (_lookInputVector.sqrMagnitude > 0f && OrientationSharpness > 0f)
    // {
    //     // Smoothly interpolate from current to target look direction
    //     Vector3 smoothedLookInputDirection = Vector3.Slerp(Motor.CharacterForward, _lookInputVector, 1 - Mathf.Exp(-OrientationSharpness * deltaTime)).normalized;

    //     // Set the current rotation (which will be used by the KinematicCharacterMotor)
    //     currentRotation = Quaternion.LookRotation(smoothedLookInputDirection, Motor.CharacterUp);
    // }

    }
    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        base.UpdateVelocity(ref currentVelocity, deltaTime);
    }


    public override void EnterState()
    {
        base.EnterState();
        _context.animator.SetBool("Running", true);
    }

    public override void ExitState()
    {
        base.ExitState();
        _context.animator.SetBool("Running", false);
    }

    public override void UpdateState()
    {

    }


}
