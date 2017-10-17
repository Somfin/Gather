using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler> {
    private Scene persistentScene;
    private Scene currentScene;
    private AsyncOperation loadTask;

    public void Start()
    {
        persistentScene = SceneManager.GetActiveScene();
    }

    public void LoadScene(string sceneName)
    {
        GameState.Instance.SetState(GameState.State.PAUSE);
        if (currentScene.name != null)
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
        loadTask = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentScene = SceneManager.GetSceneByName(sceneName);
    }

    private void Update()
    {
        if (loadTask != null && loadTask.isDone)
        {
            SceneManager.SetActiveScene(currentScene);
            GameState.Instance.SetState(GameState.State.PLAY);
            loadTask = null;
        }
    }

    public GameObject InstantiateInPersistentScene(GameObject toInstantiate)
    {
        if (currentScene != null)
        {
            SceneManager.SetActiveScene(persistentScene);
        }
        var instance = GameObject.Instantiate(toInstantiate) as GameObject;
        if (currentScene != null)
        {
            SceneManager.SetActiveScene(currentScene);
        }
        return instance;
    }
}