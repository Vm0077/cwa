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
    public Player player;
    // prevent update when the state of game is decided;
    bool _isGameStateDecided = false;

    void Awake()
    {
        DataPersistenceManager.instance.LoadGame();
    }

    void OnEnable(){
        DataPersistenceManager.instance.LoadGame();
        SceneManager.sceneLoaded += OnSceneLoaded;
        Star.OnStarCollected += GotoWinScreen;
    }
    void OnDisable(){
        DataPersistenceManager.instance.SaveGame();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Star.OnStarCollected -= GotoWinScreen;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        _isGameStateDecided = false;
        DataPersistenceManager.instance.SaveGame();
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
        if(player.life == 0 && !_isGameStateDecided) {
                GotoLoseScreen();
                _isGameStateDecided = true;
                return;
        }
        // falling of the stage
        if(player.CharacterMovement.motor.transform.position.y < -100 && !_isGameStateDecided){
            player.LoseOneLife();
            DataPersistenceManager.instance.SaveGame();
            if(player.life == 0) {
                GotoLoseScreen();
                _isGameStateDecided = true;
                return;
            }
            SceneSwapManager.instance.GoBackToHubWorld();
            _isGameStateDecided = true;
        }
    }


    public void Play() {
        currentState = GameState.Play;
    }

    public void  GotoLoseScreen(){
        currentState = GameState.Play;
        SceneSwapManager.instance.SwapToDeadScene();
    }
    public void  GotoWinScreen(){
        currentState = GameState.Play;
        SceneSwapManager.instance.SwapToResult();
    }

    public void BackToStart() {
        currentState = GameState.Play;
        SceneSwapManager.instance.SwapToStart();
    }
}
