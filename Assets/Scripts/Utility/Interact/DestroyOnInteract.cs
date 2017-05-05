using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInteract : Interaction {
    public override void Interact(GameObject interactor)
    {
        Destroy(gameObject);
    }
}
