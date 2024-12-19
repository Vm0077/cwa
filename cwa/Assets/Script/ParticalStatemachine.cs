using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ParticleStatemachine : StateMachineBehaviour
{
    public ParticleSystem particleSystem;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        particleSystem.Play();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        particleSystem.Stop();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
    }
}
