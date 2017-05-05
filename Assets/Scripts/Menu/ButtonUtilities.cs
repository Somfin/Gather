using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(targetScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}