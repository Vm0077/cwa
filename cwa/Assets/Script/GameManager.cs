using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum GameState {
     Play,
     Pause,

}
public class GameManager : MonoBehaviour
{
    GameState currentState = GameState.Play;
    Player player;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(currentState == GameState.Pause){
                currentState = GameState.Play;
            }else{
                currentState = GameState.Pause;
            }
        }
        switch (currentState)
        {
            case GameState.Pause:
            {
                Time.timeScale = 0;
                break;
            }
            case GameState.Play:
            {
                Time.timeScale = 1;
                break;
            }
        }
    }
}
