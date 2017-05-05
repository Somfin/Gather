using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassidyMenuSystem : MonoBehaviour {
    public GameObject pauseMenu;
    public GameObject inventoryMenu;

    private void Start()
    {
        GameState.Instance.SetState(GameState.State.PLAY);
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        switch (GameState.Instance.currentState)
        {
            case GameState.State.PLAY:
                GameState.Instance.SetState(GameState.State.PAUSE);
                pauseMenu.SetActive(true);
                break;
            case GameState.State.PAUSE:
                GameState.Instance.SetState(GameState.State.PLAY);
                pauseMenu.SetActive(false);
                break;
        }
    }
}
