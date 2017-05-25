using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAdapter : MonoBehaviour {
    [SerializeField]
    private CassidyInventorySystem inventory;
    private bool isDirty;
    private List<GameObject> currentItems;
    private List<GameObject> currentCraftables;

    // Update is called once per frame
    void Update () {
        if (isDirty)
        {
            isDirty = false;
        }
		if (inventory.CheckIfDirtyOnlyOnce())
        {
            currentItems = inventory.GetCurrentItems();
            currentCraftables = inventory.GetCurrentCraftables();
            isDirty = true;
        }
	}

    public bool IsDirty()
    {
        return isDirty;
    }

    public List<GameObject> GetInventoryState()
    {
        return currentItems;
    }

    public List<GameObject> GetCurrentCraftables()
    {
        return currentCraftables;
    }

    public void RequestCraftItem(GameObject item)
    {
        inventory.CraftItem(item);
    }
}
