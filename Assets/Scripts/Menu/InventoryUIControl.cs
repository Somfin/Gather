using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class InventoryUIControl : MonoBehaviour {
    public float itemHeight;
    public CassidyInventorySystem inventory;
    public GameObject buttonPrototype;

    private RectTransform rect;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, 0);
	}

    private void Update()
    {
        if (inventory.CheckIfDirtyOnlyOnce())
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            var items = inventory.items.Select(i => i.GetComponent<Item>());
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, items.Count() * itemHeight);
            foreach (Item i in items)
            {
                var button = Instantiate(buttonPrototype);
                var text = button.GetComponentInChildren<Text>();
                text.text = i.name;
                button.transform.SetParent(transform, false);
            }
        }
    }
}
