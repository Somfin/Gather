using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectOnDestroy : MonoBehaviour {
    public GameObject createThis;
    private bool doNotCreate = false;
    private GameObject createdInstance;

    private void OnDestroy()
    {
        if (doNotCreate || GameState.Instance.currentState == GameState.State.MENU)
        {
            return;
        }
        createdInstance = GameObject.Instantiate(createThis);
        createdInstance.transform.position = gameObject.transform.position;
    }

    private void OnApplicationQuit()
    {
        doNotCreate = true;
    }
}
