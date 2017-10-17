using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemAssembler {
    private List<GameObject> allItems;
    private string[] paths = new string[] {
        "Prefabs\\Items\\Weapon Parts",
        "Prefabs\\Weapons"
    };

    public ItemAssembler()
    {
        allItems = new List<GameObject>();
        foreach(var path in paths)
        {
            var items = Resources.LoadAll<GameObject>(path);
            allItems = allItems.Concat(items).ToList();
        }
    }

    public GameObject RetrieveItem(string key)
    {
        return allItems.FirstOrDefault(i => i.name == key);
    }
}
