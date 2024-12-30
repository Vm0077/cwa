using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreGamePlayUI : MonoBehaviour {
  [SerializeField] public TMP_Text CoinText;
  [SerializeField] public TMP_Text HeartText;
  [SerializeField] public TMP_Text StarText;
  [SerializeField] public TMP_Text TimeText;
  public Player player;
  void Awake(){

  }
  void Update() {
      UpdateCoin();
      UpdateHeart();
      UpdateStar();
  }
  void UpdateCoin() {
    this.CoinText.text = player.coin.ToString().PadLeft(3,'0') + "x";
  }

  void UpdateHeart() {
    this.HeartText.text = player.life.ToString().PadLeft(3,'0') + "x";
  }
  void UpdateStar() {
    this.StarText.text = player.star.ToString()+ "x";
  }
}
