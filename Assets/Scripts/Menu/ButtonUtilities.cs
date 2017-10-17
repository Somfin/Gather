using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUtilities : MonoBehaviour {
    [SerializeField]
    private GameObject cassidy;

    public void SetPause()
    {
        SetState(GameState.State.PAUSE);
    }

    public void SetPlay()
    {
        SetState(GameState.State.PLAY);
    }

    public void SetMenu()
    {
        SetState(GameState.State.MENU);
    }

    private void SetState(GameState.State state)
    {
        GameState.Instance.SetState(state);
    }

    public void LoadScene(string targetScene)
    {
        SceneHandler.Instance.LoadScene(targetScene);
    }

    public void DespawnCassidy()
    {
        Destroy(GameState.Instance.cassidy);
    }

    public void StartFile(int file)
    {
        GameState.Instance.SetFile(file);
        var save = new GameSave("Test Scene", 0, 1, new List<string>(), new List<string>());
    }

    public bool HasFileToLoad(int i)
    {
        return GameSave.HasFile((GameState.File)i);
    }

    public void LoadFile(int file)
    {
        GameState.Instance.SetFile(file);
        GameSave.LoadFromFile(cassidy, GameSave.CurrentFilePath());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}