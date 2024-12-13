using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class CharacterContext
{
    public KinematicCharacterMotor Motor;
    public Animator animator;
    public bool isJumpPressed = false;
    public Vector3 currentVelocity;
    public PlayerCharacterInputs inputs;
    public CharacterContext(KinematicCharacterMotor motor, Animator animator){
        this.Motor = motor;
        this.animator = animator;
    }
}
