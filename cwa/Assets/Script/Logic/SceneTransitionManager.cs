using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SceneTransitionManager : MonoBehaviour

{
    public static SceneTransitionManager Instance;
    public GameObject transitionsContainer;
    private SceneTransition[] transitions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        transitions =
            transitionsContainer.GetComponentsInChildren<SceneTransition>();
        transitionsContainer.SetActive(false);
    }

    public void LoadScene(List<AsyncOperation>scene, string transitionName)
    {
        transitionsContainer.SetActive(true);
        StartCoroutine(TransitionAsync(scene, transitionName));
    }

    private IEnumerator TransitionAsync(List<AsyncOperation> scene, string transitionName)
    {
        SceneTransition transition =
            transitions.First(t => t.name == transitionName);
        for (int i = 0; i < scene.Count ; i++ ) {
            scene[i].allowSceneActivation = false;
        }

        yield return transition.AnimateTransitionIn();

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < scene.Count ; i++ ) {
            scene[i].allowSceneActivation = true;
        }

        yield return transition.AnimateTransitionOut();
        transitionsContainer.SetActive(false);
    }
}
