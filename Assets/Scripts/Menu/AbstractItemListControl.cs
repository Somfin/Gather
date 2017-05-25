﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public abstract class AbstractItemListControl : MonoBehaviour {
    public float itemHeight;
    public GameObject buttonPrototype;

    protected InventoryAdapter inventory;

    private RectTransform rect;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, 0);
        inventory = GetComponentInParent<InventoryAdapter>();
	}

    private void Update()
    {
        if (inventory.IsDirty())
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            var items = GetContent();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, items.Count() * itemHeight);
            foreach (GameObject i in items)
            {
                var button = Instantiate(buttonPrototype);
                SetupButton(i, button);
                button.transform.SetParent(transform, false);
            }
        }
    }   

    public abstract List<GameObject> GetContent();
    public abstract void SetupButton(GameObject item, GameObject button);
}
