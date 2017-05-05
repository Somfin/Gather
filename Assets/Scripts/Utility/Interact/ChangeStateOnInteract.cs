using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateOnInteract : Interaction
{
    public GameState.State state;

    public override void Interact(GameObject interactor)
    {
        GameState.Instance.SetState(state);
    }
}
