using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class LoadoutListControl : AbstractItemListControl {
    public override List<GameObject> GetContent()
    {
        return inventory.GetCurrentLoadout().ToList();
    }

    public override void SetupButton(GameObject item, GameObject button)
    {
        var text = button.GetComponentInChildren<Text>();
        text.text = item.name;
    }

    public override void SwapLeft(int index)
    {
        inventory.MoveLoadoutPosition(index, index-1);
    }

    public override void SwapRight(int index)
    {
        inventory.MoveLoadoutPosition(index, index+1);
    }

    public override void SwapOut(int index)
    {
        inventory.RemoveFromLoadout(index);
    }
}
