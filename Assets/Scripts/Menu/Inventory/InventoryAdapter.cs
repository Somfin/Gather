using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryAdapter : MonoBehaviour {
    public CassidyInventorySystem inventory;
    public CassidyCombat combat;
    
    private int updateNumber;
    private int loadoutSelection;
    private List<GameObject> currentItems;
    private List<GameObject> currentCraftables;
    private List<GameObject> currentLoadout;

    private void Start()
    {
        updateNumber = 0;
    }

    void Update () {
		if (inventory.CheckIfDirtyOnlyOnce())
        {
            currentItems = inventory.GetCurrentItems();
            currentCraftables = inventory.GetCurrentCraftables();
            updateNumber += 1;
        }
        if (combat.CheckIfDirtyOnlyOnce())
        {
            currentLoadout = combat.GetCurrentLoadout();
            loadoutSelection = combat.equippedIndex;
            updateNumber += 1;
        }
	}

    public int CurrentUpdate()
    {
        return updateNumber;
    }

    public List<GameObject> GetInventoryState()
    {
        if (currentItems == null)
        {
            return new List<GameObject>();
        }
        return currentItems;
    }

    public List<GameObject> GetCurrentCraftables()
    {
        if (currentCraftables == null)
        {
            return new List<GameObject>();
        }
        return currentCraftables;
    }

    public List<GameObject> GetCurrentLoadout()
    {
        if (currentLoadout == null)
        {
            return new List<GameObject>();
        }
        return currentLoadout;
    }

    public void RequestCraftItem(GameObject item)
    {
        inventory.CraftItem(item);
    }

    public void MoveLoadoutPosition(int indexSource, int indexTarget)
    {
        var target = getLoadoutItemAtIndex(indexSource);
        combat.RemoveFromLoadout(target);
        combat.MoveItemToPosition(target, indexTarget);
    }

    public void RemoveFromLoadout(int index)
    {
        var target = getLoadoutItemAtIndex(index);
        combat.RemoveFromLoadout(target);
    }

    public void AddToLoadout(int index)
    {
        var target = getItemAtIndex(index);
        combat.AddToLoadout(target);
    }

    public GameObject getItemAtIndex(int index)
    {
        return currentItems.Where(i => i.GetComponent<Item>().itemType == Item.Type.WEAPON).ToList()[index];
    }

    public GameObject getLoadoutItemAtIndex(int index)
    {
        return currentLoadout[index];
    }
}
