using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : Singleton<GameState> {
    public State currentState
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

    public void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 200, 20), "Current state: " + currentState.ToString());
    }
}