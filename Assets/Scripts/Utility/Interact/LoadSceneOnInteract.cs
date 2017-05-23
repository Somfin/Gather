using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnInteract : Interaction {
    public string sceneName;
    public override void Interact(GameObject interactor)
    {
        SceneHandler.Instance.LoadScene(sceneName);
    }
}
