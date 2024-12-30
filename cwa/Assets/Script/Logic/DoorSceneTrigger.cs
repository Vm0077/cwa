using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  DoorSceneTrigger :MonoBehaviour{
    [Header("Spawn To")] private Transform position;

    [SerializeField] private SceneField _sceneToLoad;
    [SerializeField] public string exitName;
    public bool _needStarToUnlock;

    public void Interace() {

    }
    void OnTriggerEnter(Collider other) {
        if(_needStarToUnlock && Player.instance.star <=0) {
            return;
        }
        if(other.CompareTag("Player")){
            PlayerPrefs.SetString("LastExitName", exitName);
            PlayerPrefs.Save();
            SceneSwapManager.instance.SwapSceneFromDoor(_sceneToLoad);
        }
    }

    private void LoadScene () {
                SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
    }
}
