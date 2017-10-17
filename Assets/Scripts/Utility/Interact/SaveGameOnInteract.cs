using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameOnInteract : Interaction{

    public override void Interact(GameObject interactor)
    {
        var save = new GameSave(interactor, gameObject);
        save.Save();
    }
}