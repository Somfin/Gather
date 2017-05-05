using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CassidyCombat))]
public class CassidyInventorySystem : MonoBehaviour {
    public GameObject inventoryMenu;
    public List<GameObject> items;
    public List<GameObject> craftables;

    private CassidyCombat combat;

    private void Start()
    {
        combat = GetComponent<CassidyCombat>();
    }

    // Update is called once per frame
    void Update () {
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
                break;
            case GameState.State.PAUSE:
                GameState.Instance.SetState(GameState.State.PLAY);
                inventoryMenu.SetActive(false);
                break; 
        }
    }

    private void Craft()
    {
        var resources = items.Select(i => i.GetComponent<Item>().part).ToList();
        var working = craftables.Where(c => c.GetComponent<Item>().recipe.All(r => resources.Contains(r))).ToList();
        foreach (GameObject o in working)
        {
            var itemsToRemove = items.Where(i => o.GetComponent<Item>().recipe.Contains(i.GetComponent<Item>().part)).ToList();
            foreach (GameObject i in itemsToRemove)
            {
                if (i.GetComponent<Item>().itemType == Item.Type.WEAPON)
                {
                    combat.loadout.Remove(i);
                }
                items.Remove(i);
            }
            AddItem(o);
        }
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
        }
    }
}
