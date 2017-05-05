using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItemOnInteract : Interaction {
    public GameObject heldItem;

    public override void Interact(GameObject interactor)
    {
        var inventory = interactor.GetComponent<CassidyInventorySystem>();
        inventory.AddItem(heldItem);
    }
}
