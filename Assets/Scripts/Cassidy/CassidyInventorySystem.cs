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

    private List<GameObject> items;
    private bool isDirty;

    private void Start()
    {
        GameState.Instance.SetState(GameState.State.PLAY);
        this.items = new List<GameObject>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleMenu();
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
                combat.loadout.Remove(i);
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
                combat.loadout.Add(item);
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
