using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler> {
    private Scene currentScene;
    private AsyncOperation loadTask;

    public void LoadScene(string sceneName)
    {
        if (currentScene.name != null)
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
        loadTask = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentScene = SceneManager.GetSceneByName(sceneName);
    }

    private void Update()
    {
        if (loadTask.isDone)
        {
            SceneManager.SetActiveScene(currentScene);
        }
    }
}