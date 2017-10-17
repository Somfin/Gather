using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CassidyInventorySystem : MonoBehaviour {
    public GameObject inventoryMenu;
    public CassidyCombat combat;
    public List<GameObject> craftables;

    private List<GameObject> items = new List<GameObject>();
    private bool isDirty = true;

    void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleMenu();
        }
	}

    private void ToggleMenu()
    {
        if (GameState.Instance.CanToggleSingletonMenu(inventoryMenu))
        {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
            if (inventoryMenu.activeSelf)
            {
                GameState.Instance.RegisterMenuOpen(inventoryMenu);
            }
            else
            {
                GameState.Instance.RegisterMenuClose(inventoryMenu);
            }
        }
    }

    public List<GameObject> GetCurrentItems()
    {
        return items;
    }

    public List<GameObject> GetCurrentCraftables()
    {
        var resources = items.Select(i => i.GetComponent<Item>().part).ToList();
        var working = craftables.Where(c => c.GetComponent<Item>().recipe.All(r => resources.Contains(r))).ToList();
        return working;
    }

    public void CraftItem(GameObject item)
    {
        Assert.IsTrue(GetCurrentCraftables().Contains(item));

        var itemsToRemove = items.Where(i => item.GetComponent<Item>().recipe.Contains(i.GetComponent<Item>().part)).ToList();
        foreach (GameObject i in itemsToRemove)
        {
            if (i.GetComponent<Item>().itemType == Item.Type.WEAPON)
            {
                combat.RemoveFromLoadout(i);
            }
            items.Remove(i);
        }
        AddItem(item);
    }

    public void AddItem (GameObject item)
    {
        var itemComponent = item.GetComponent<Item>();
        if (itemComponent != null)
        {
            if (itemComponent.itemType == Item.Type.WEAPON)
            {
                combat.AddToLoadout(item);
            }
            items.Add(item);
            isDirty = true;
        }
    }

    public bool CheckIfDirtyOnlyOnce()
    {
        if (isDirty)
        {
            isDirty = false;
            return true;
        }
        return false;
    }
}
