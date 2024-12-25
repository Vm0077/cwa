using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Animator animator;
    //public ParticleSystem particle;
    public int running;
    public int velocityXanim;
    public int velocityZanim;
    public int jumpCount;
    public int jumping;
    public int attack;
    public int idle;
    public int falling;
    public int crouch;


    public int attackCount;
    void SetUpHashAnimation()
    {
        running = Animator.StringToHash("Running");
        jumping = Animator.StringToHash("Jumping");
        velocityXanim = Animator.StringToHash("VelocityRight");
        velocityZanim = Animator.StringToHash("VelocityForward");
        idle = Animator.StringToHash("Idle");
        falling = Animator.StringToHash("Falling");
        jumpCount = Animator.StringToHash("JumpCount");
        crouch = Animator.StringToHash("Crouch");
        attackCount = Animator.StringToHash("AttackCount");
        attack = Animator.StringToHash("Attacking");
    }

    void Start()
    {
        SetUpHashAnimation();
        //ParticleStatemachine particleStatemachine = animator.GetBehaviour<ParticleStatemachine>();
        //particleStatemachine.particleSystem = particle;
    }
    void Update()
    {

    }

     public void SetRunVelocity(Vector3 vector)
    {
        animator.SetFloat(velocityZanim, vector.z);
        animator.SetFloat(velocityXanim, vector.x);
    }

    public void SetJumpCount(int count)
    {
        animator.SetInteger(jumpCount, count);
    }

    public void Attack(){
        animator.SetTrigger(attack);
    }
    public void SetAttactCount(int count){
        animator.SetInteger(attackCount,count);
    }
}
