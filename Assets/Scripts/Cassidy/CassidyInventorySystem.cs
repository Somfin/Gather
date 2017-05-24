using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CassidyInventorySystem : MonoBehaviour {
    public GameObject inventoryMenu;
    public CassidyCombat combat;
    public List<GameObject> items;
    public List<GameObject> craftables;
    private bool isDirty;
    private bool beenRead;

    // Update is called once per frame
    void Update () {
        if (beenRead)
        {
            isDirty = false;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleMenu();
        }
		if (Input.GetKeyDown(KeyCode.C))
        {
            Craft();
        }
	}

    private void ToggleMenu()
    {
        switch (GameState.Instance.currentState)
        {
            case GameState.State.PLAY:
                GameState.Instance.SetState(GameState.State.PAUSE);
                inventoryMenu.SetActive(true);
                foreach (Scrollbar sb in inventoryMenu.GetComponentsInChildren<Scrollbar>())
                {
                    sb.onValueChanged.Invoke(1);
                }
                break;
            case GameState.State.PAUSE:
                GameState.Instance.SetState(GameState.State.PLAY);
                inventoryMenu.SetActive(false);
                break; 
        }
    }

    private void Craft()
    {
        foreach (GameObject item in GetCurrentCraftables())
        {
            CraftItem(item);
        }
    }

    public void CraftItem(GameObject item)
    {
        var itemsToRemove = items.Where(i => item.GetComponent<Item>().recipe.Contains(i.GetComponent<Item>().part)).ToList();
        foreach (GameObject i in itemsToRemove)
        {
            if (i.GetComponent<Item>().itemType == Item.Type.WEAPON)
            {
                combat.loadout.Remove(i);
            }
            items.Remove(i);
        }
        AddItem(item);
        isDirty = true;
        beenRead = false;
    }

    public void AddItem (GameObject itemComp)
    {
        var item = itemComp.GetComponent<Item>();
        if (item != null)
        {
            if (item.itemType == Item.Type.WEAPON)
            {
                combat.loadout.Add(itemComp);
            }
            items.Add(itemComp);
            isDirty = true;
            beenRead = false;
        }
    }

    public List<GameObject> GetCurrentCraftables()
    {
        var resources = items.Select(i => i.GetComponent<Item>().part).ToList();
        var working = craftables.Where(c => c.GetComponent<Item>().recipe.All(r => resources.Contains(r))).ToList();
        return working;
    }

    public void OnGUI()
    {
        var names = string.Join(",", GetCurrentCraftables().Select(i => i.name).ToArray());
        GUI.TextField(new Rect(0, 0, 200, 20), "Can craft: " + names);
    }

    public bool CheckIfDirtyOnlyOnce()
    {
        if (isDirty)
        {
            beenRead = true;
            return true;
        }
        return false;
    }
}
