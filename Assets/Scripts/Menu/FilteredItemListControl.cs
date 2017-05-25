using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class FilteredItemListControl : AbstractItemListControl
{
    public SType typeFilter;
    [Serializable]
    public class SType : SerializableEnum<Item.Type> { }

    public override List<GameObject> GetContent()
    {
        return inventory.GetInventoryState().Where(i => i.GetComponent<Item>().itemType == typeFilter.Value).ToList();
    }

    public override void SetupButton(GameObject item, GameObject button)
    {
        var text = button.GetComponentInChildren<Text>();
        text.text = item.GetComponent<Item>().name;
    }
}
