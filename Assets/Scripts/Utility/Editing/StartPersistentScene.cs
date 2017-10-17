using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPersistentScene : MonoBehaviour {
    public string startSceneName;

	// Use this for initialization
	void Start () {
        SceneHandler.Instance.LoadScene(startSceneName);
	}
}
