using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour {
  [SerializeField]
  AudioSource SFXSource;
  [SerializeField]
  CharacterMovementStateMachine stateMachine;
  public static CharacterAudioManager instance;
  public AudioClip JumpSound1;
  public AudioClip JumpSound2;
  public AudioClip CoinSound;
  public AudioClip HeartSound;
  public AudioClip StarSound;
  void Awake() {
  }

  void Start() {}
  void Update() {}
  private void OnEnable() {
    SetUpEvent();
  }
  private void OnDestroy() {
    Debug.Log("OnDestroy");
    UnsubscribeEvent();
  }

  void ItemCollected() {
    SFXSource.clip = CoinSound;
    SFXSource.Play();
  }


  void SetUpEvent() { Coin.OnCoinCollected += ItemCollected; }
  void UnsubscribeEvent() {
    Coin.OnCoinCollected -= ItemCollected;
  }

  void JumpSound() {
    switch (stateMachine._context.jumpCount) {
    case 1:
    case 2: {
      SFXSource.clip = JumpSound1;
      break;
    }
    case 3: {
      SFXSource.clip = JumpSound2;
      Debug.Log("play 2");
      break;
    }
    default: {
      break;
    }
    }
    SFXSource.Play();
  }
}
