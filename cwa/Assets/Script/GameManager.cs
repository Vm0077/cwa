using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameState {
     Play,
     Pause,
}
public class GameManager : MonoBehaviour
{
    GameState currentState = GameState.Play;
    public Canvas GameplayCanvas;
    public Canvas PauseCanvas;
    [SerializeField] private string _persistenceGameplay = "PersistenceGameplay";
    [SerializeField] private string _loadScene = "Hubworld-1";
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
                GameplayCanvas.gameObject.SetActive(false);
                PauseCanvas.gameObject.SetActive(true);
                break;
            }
            case GameState.Play:
            {
                Time.timeScale = 1;
                GameplayCanvas.gameObject.SetActive(true);
                PauseCanvas.gameObject.SetActive(false);
                break;
            }
        }
    }


    public void Play() {
        currentState = GameState.Play;
    }

    public void BackToStart() {
        SceneManager.LoadScene("StartScreenPrototype");
    }
}
