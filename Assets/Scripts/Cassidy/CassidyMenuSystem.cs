using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassidyMenuSystem : MonoBehaviour {
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        if (GameState.Instance.currentState != GameState.State.MENU)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (pauseMenu.activeSelf)
            {
                GameState.Instance.RegisterMenuOpen(pauseMenu);
            }
            else
            {
                GameState.Instance.RegisterMenuClose(pauseMenu);
            }
        }
    }
}
