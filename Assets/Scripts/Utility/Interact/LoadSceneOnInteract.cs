using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInteract : Interaction {
    public string sceneName;
    public override void Interact(GameObject interactor)
    {
        SceneManager.LoadScene(sceneName);
    }
}
