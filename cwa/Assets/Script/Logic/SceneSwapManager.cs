using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;
    public SceneField PersistenceGamplay;
    public SceneField DeadScene;
    public SceneField ResultScene;
    public SceneField HubworldScene;
    public SceneField StartScreen;
    public Transform SpawnPoint;
    bool _isFromDoor;
    bool _isSpawnBack;
    void Start()
    {
        if (instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public void SwapSceneFromDoor(SceneField sceneField)
    {
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        _isFromDoor = true;
        Player.instance.CharacterMovement._context.allowToMove=false;
        scenes.Add(SceneManager.LoadSceneAsync(sceneField.SceneName, LoadSceneMode.Additive));
        UnloadSceneExcept(sceneField.SceneName);
        SceneTransitionManager.Instance.LoadScene(scenes,"CrossFade");

    }

    public void SwapScene(SceneField sceneField)
    {
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        Player.instance.CharacterMovement._context.allowToMove=true;
        scenes.Add(SceneManager.LoadSceneAsync(sceneField.SceneName, LoadSceneMode.Single));
        SceneTransitionManager.Instance.LoadScene(scenes,"CrossFade");

    }
    public static void SwapScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SwapToDeadScene()
    {
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        scenes.Add( SceneManager.LoadSceneAsync(DeadScene));
        SceneTransitionManager.Instance.LoadScene(scenes,"CrossFade");
    }
    public  void SwapToResult()
    {
        SceneManager.LoadScene(ResultScene);
    }
    public  void SwapToStart()
    {
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        scenes.Add( SceneManager.LoadSceneAsync(StartScreen));
        SceneTransitionManager.Instance.LoadScene(scenes,"CrossFade");
    }
    public void StartHubWorld()
    {
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        scenes.Add(SceneManager.LoadSceneAsync(PersistenceGamplay, LoadSceneMode.Single));
        scenes.Add(SceneManager.LoadSceneAsync(HubworldScene, LoadSceneMode.Additive));
        SceneTransitionManager.Instance.LoadScene(scenes,"CrossFade");
    }
    public void GoBackToHubWorld()
    {
        _isSpawnBack = true;
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        scenes.Add(SceneManager.LoadSceneAsync(HubworldScene, LoadSceneMode.Additive));
        UnloadSceneExcept(HubworldScene);
        SceneTransitionManager.Instance.LoadScene(scenes,"CrossFade");
    }
   public void GoBackToHubWorldFromWin()
    {
        _isSpawnBack = true;
        SceneManager.UnloadSceneAsync(ResultScene);
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        scenes.Add(SceneManager.LoadSceneAsync(HubworldScene, LoadSceneMode.Single));
        scenes.Add(SceneManager.LoadSceneAsync(PersistenceGamplay, LoadSceneMode.Additive));
        SceneTransitionManager.Instance.LoadScene(scenes,"CrossFade");
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        if(_isFromDoor || _isSpawnBack){
            FindNextSceneSwapPoint();
            CalculatePlayerPosition();
            _isFromDoor = false;
            _isSpawnBack = false;
            Player.instance.CharacterMovement._context.allowToMove=true;
        }
    }

    private void FindNextSceneSwapPoint()
    {
        DoorSceneTrigger[] triggers = FindObjectsByType<DoorSceneTrigger>(FindObjectsSortMode.InstanceID);
        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i].exitName == PlayerPrefs.GetString("LastExitName"))
            {
                SpawnPoint = triggers[i].GetComponent<Transform>();
                Debug.Log(SpawnPoint.position);
                break;
            };
        }
    }
    private void CalculatePlayerPosition()
    {
       Transform adjustSpawnPoint = SpawnPoint;
       adjustSpawnPoint.transform.position = SpawnPoint.transform.position + (SpawnPoint.forward * 5);
       Player.instance.Respawn(SpawnPoint);
    }
    private void UnloadSceneExcept (string name) {
        for(int j = 0 ;  j < SceneManager.sceneCount ; j++ ){
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if(loadedScene.name != PersistenceGamplay.SceneName && loadedScene.name != name ){
                    SceneManager.UnloadSceneAsync(loadedScene);
                }
        }

    }
}
