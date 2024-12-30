using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour{
    [Header("Spawn To")] private Transform position;
    [SerializeField] private SceneField[] _sceneToLoad;
    [SerializeField] private SceneField[] _sceneToUnload;

    public void Interace() {

    }
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            LoadScene();
            UnloadScene();
        }
    }

    private void LoadScene () {
        for(int i = 0 ;  i < _sceneToLoad.Length ; i++ ){
            bool isSceneLoaded = false;
            for(int j = 0 ;  j < SceneManager.sceneCount ; j++ ){
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if(loadedScene.name == _sceneToLoad[i].SceneName){
                    isSceneLoaded = true;
                    break;
                }
            }
            if(!isSceneLoaded){
                SceneManager.LoadSceneAsync(_sceneToLoad[i], LoadSceneMode.Additive);
            }
        }
    }
    private void UnloadScene () {
        for(int i = 0 ;  i < _sceneToLoad.Length ; i++ ){
            for(int j = 0 ;  j < SceneManager.sceneCount ; j++ ){
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if(loadedScene.name == _sceneToUnload[i].SceneName){
                    SceneManager.UnloadSceneAsync(_sceneToUnload[i]);
                }
            }
        }
    }
}
