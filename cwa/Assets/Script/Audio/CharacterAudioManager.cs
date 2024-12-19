using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] CharacterStateMachine stateMachine;
    public AudioClip JumpSound1;
    public AudioClip JumpSound2;
    void Start()
    {

    }

    void Update()
    {

    }

    void JumpSound(){
        switch (stateMachine._context.jumpCount) {
            case 1 :
            case 2 :{
                musicSource.clip = JumpSound1;
                break;
            }
            case 3 :{
                musicSource.clip = JumpSound2;
                break;
            }


        }
        musicSource.Play();
    }
}
