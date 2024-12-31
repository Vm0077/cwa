using System;
using TMPro;
using UnityEngine;

public class ResultData : MonoBehaviour,IDataPersistence
{
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text starText;

    int coin;
    int star;
    float timeToDisplay;

    public void LoadData(GameData data)
    {
        coin = data.coin;
        star = data.star;
        timeToDisplay = data.time;
    }

    public void SaveData(GameData data)
    {
    }

    private void Start() {
      UpdateCoin();
      UpdateTime();
      UpdateStar();
    }
  void UpdateCoin() {
    this.coinText.text = "Coins: " + coin.ToString().PadLeft(3,'0');
  }

  void UpdateStar() {
    this.starText.text = "Stars : " + star.ToString().PadLeft(3,'0');
  }
  void UpdateTime() {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        timeText.text = "Time: " + timeSpan.ToString(@"mm\:ss\:ff");
  }

  public void GoBackToStart(){
    SceneSwapManager.instance.SwapToStart();
  }
  public void GoBackToHubWorld(){
    Debug.Log("click");
    SceneSwapManager.instance.GoBackToHubWorldFromWin();
  }
}
