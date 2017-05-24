using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class CraftableListControl : AbstractItemListControl {
    public override List<GameObject> GetContent()
    {
        return inventory.GetCurrentCraftables().ToList();
    }

    public override void SetupButton(GameObject item, GameObject button)
    {
        var text = button.GetComponentInChildren<Text>();
        text.text = item.name;
        var craftFunction = button.GetComponent<CraftCraftable>();
        craftFunction.inventory = inventory;
        craftFunction.craftable = item;
    }
}
