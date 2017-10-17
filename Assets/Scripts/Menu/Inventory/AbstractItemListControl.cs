using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public abstract class AbstractItemListControl : MonoBehaviour {
    public float itemSize;
    public float itemSpacing;
    public bool isVertical;
    public GameObject buttonPrototype;

    public bool allowSwapUp;
    public bool allowSwapDown;
    public bool allowSwapSideways;

    protected InventoryAdapter inventory;

    private RectTransform rect;
    private LayoutGroup layout;
    private int updateNumber;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, 0);

        layout = GetComponent<LayoutGroup>();

        inventory = GetComponentInParent<InventoryAdapter>();

        updateNumber = -1;
	}

    private void Update()
    {
        if (inventory.CurrentUpdate() > updateNumber)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            var items = GetContent();
            var newSize = items.Count() * itemSize + (items.Count() - 1) * itemSpacing;
            rect.sizeDelta = isVertical ? new Vector2(rect.sizeDelta.x, newSize) : new Vector2(newSize, rect.sizeDelta.y);
            foreach (GameObject i in items)
            {
                var button = Instantiate(buttonPrototype);
                SetupButton(i, button);
                button.transform.SetParent(transform, false);
            }
            updateNumber = inventory.CurrentUpdate();
        }
    }   

    public abstract List<GameObject> GetContent();
    public abstract void SetupButton(GameObject item, GameObject button);
    public virtual void SwapLeft(int index) { }
    public virtual void SwapRight(int index) { }
    public virtual void SwapIn(int index) { }
    public virtual void SwapOut(int index) { }
}
