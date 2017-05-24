using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftCraftable : MonoBehaviour {
    public GameObject craftable;
    public CassidyInventorySystem inventory;

    public void Craft()
    {
        inventory.CraftItem(craftable);
    }
}
