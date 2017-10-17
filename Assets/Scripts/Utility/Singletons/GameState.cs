using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : Singleton<GameState> {
    private List<GameObject> openMenus = new List<GameObject>();

    public File currentFile
    {
        get;
        private set;
    }

    public State currentState
    {
        get;
        private set;
    }

    public GameObject cassidy
    {
        get;
        private set;
    }

    public enum State
    {
        PLAY,
        PAUSE,
        MENU
    }

    public enum File
    {
        FILE1,
        FILE2,
        FILE3
    }

    public void SetFile(int file)
    {
        currentFile = (File)file;
    }

    public void SetState(State state)
    {
        currentState = state;
        switch (state)
        {
            case State.PLAY:
                Time.timeScale = 1;
                break;
            case State.MENU:
                Time.timeScale = 1;
                break;
            case State.PAUSE:
                Time.timeScale = 0;
                break;
        }
    }

    public bool CanToggleSingletonMenu(GameObject menu)
    {
        return openMenus.Count == 0 || (openMenus[0] == menu && openMenus.Count == 1);
    }

    public void RegisterMenuOpen(GameObject menu)
    {
        openMenus.Add(menu);
        if (openMenus.Count > 0)
        {
            SetState(State.PAUSE);
        }
    }

    public void RegisterMenuClose(GameObject menu)
    {
        openMenus.Remove(menu);
        if (openMenus.Count == 0)
        {
            SetState(State.PLAY);
        }
    }

    public void SetCassidy(GameObject cassidy)
    {
        this.cassidy = cassidy;
    }
}