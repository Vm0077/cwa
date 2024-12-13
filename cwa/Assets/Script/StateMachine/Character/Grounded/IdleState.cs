using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class IdleState : GroundState
{
    CharacterContext _context;
    Vector3 Gravity = new Vector3(0f,-30f,0f);

    public IdleState(CharacterContext context):base(context){
        _context = context;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {

    }
    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        base.UpdateVelocity(ref  currentVelocity,  deltaTime);
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
