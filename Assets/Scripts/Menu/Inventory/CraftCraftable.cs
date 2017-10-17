using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftCraftable : MonoBehaviour {
    public GameObject craftable;
    public InventoryAdapter inventory;

    public void Craft()
    {
        inventory.RequestCraftItem(craftable);
    }
}
