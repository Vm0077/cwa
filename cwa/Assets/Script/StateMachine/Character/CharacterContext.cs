using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class CharacterContext
{
    public KinematicCharacterMotor Motor;
    public bool _jumpRequested = false;
    public Vector3 currentVelocity;
    public Vector3 _lookInputVector;
    public PlayerCharacterInputs inputs;
    public CharacterAnimationController animationController;
    public CharacterContext(KinematicCharacterMotor motor, CharacterAnimationController characterAnimation){
        this.Motor = motor;
        this.animationController = characterAnimation;
    }
}
