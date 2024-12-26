using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    [SerializeField] CharacterMovementStateMachine stateMachine;
    public AudioClip JumpSound1;
    public AudioClip JumpSound2;
    public AudioClip CoinSound;
    public AudioClip HeartSound;
    public AudioClip StarSound;
    void Start()
    {
        SetUpEvent();
    }

    void Update()
    {

    }

    void ItemCollected () {
        SFXSource.clip = CoinSound;
        SFXSource.Play();
    }
    void SetUpEvent () {
        Coin.OnCoinCollected += ItemCollected;
    }

    void JumpSound(){
        switch (stateMachine._context.jumpCount) {
            case 1 :
            case 2 :{
                SFXSource.clip = JumpSound1;
                break;
            }
            case 3 :{
                SFXSource.clip = JumpSound2;
                Debug.Log("play 2");
                break;
            }
            default :{
                break;
            }
        }
        SFXSource.Play();
    }
}
