using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour{
    [SerializeField] private SceneField[] _sceneToLoad;
    [SerializeField] private SceneField[] _sceneToUnload;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            LoadScene();
            UnloadScene();
        }
    }
    private void LoadScene() {
        bool isSceneLoaded = false;
        for(int i = 0;i < _sceneToUnload.Length; i++) {
            for(int j = 0;j < SceneManager.sceneCount; j++) {
                Scene loadScene = SceneManager.GetSceneAt(j);
                if(loadScene.name == _sceneToLoad[i].SceneName){
                   isSceneLoaded = true;
                   break;
                }
            }
            if(!isSceneLoaded){
                SceneManager.LoadSceneAsync(_sceneToLoad[i],LoadSceneMode.Additive);
            }
        }
    }
    private void UnloadScene() {

    }
}
