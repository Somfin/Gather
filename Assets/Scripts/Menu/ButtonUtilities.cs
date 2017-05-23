using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUtilities : MonoBehaviour {

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

    public void ExitGame()
    {
        Application.Quit();
    }
}