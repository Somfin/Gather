using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CassidyCombat : AbstractCombatHandler {
    public int equippedIndex;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Transform source;
    [HideInInspector]
    public GameObject meleeWeaponItem;

    private List<GameObject> loadout = new List<GameObject>();

    private bool isDirty = true;

    // Update is called once per frame
    void Update ()
    {
        if (GameState.Instance.currentState == GameState.State.PLAY)
        {
            var meleeWeapon = meleeWeaponItem.GetComponent<IWeapon>();
            meleeWeapon.Ready(target.position, source.position, gameObject);
            if (Input.GetMouseButtonDown(0))
            {
                meleeWeapon.Pull(gameObject);
            }
            else
            {
                meleeWeapon.Release(gameObject);
            }

            var currentWeaponItem = loadout.ElementAtOrDefault(equippedIndex);
            if (currentWeaponItem == null)
            {
                return;
            }
            var currentWeapon = currentWeaponItem.GetComponent<IWeapon>();
            currentWeapon.Ready(target.position, source.position, gameObject);
            if (Input.GetMouseButton(1))
            {
                currentWeapon.Pull(gameObject);
            }
            else
            {
                currentWeapon.Release(gameObject);
            }
        }
        var change = Input.GetAxisRaw("Mouse Wheel");
        if (change != 0)
        {
            equippedIndex = (loadout.Count() + equippedIndex + Math.Sign(change)) % loadout.Count();
        }
    }

    public void AddToLoadout(GameObject item)
    {
        if (loadout.Contains(item))
        {
            return;
        }
        if (item != null)
        {
            loadout.Add(item);
            isDirty = true;
        }
    }

    public void RemoveFromLoadout(GameObject item)
    {
        loadout.Remove(item);
        isDirty = true;
    }

    public void MoveItemToPosition(GameObject item, int index)
    {
        loadout.Insert(index, item);
        isDirty = true;
    }

    public void ClearLoadout()
    {
        loadout = new List<GameObject>();
        isDirty = true;
    }

    public List<GameObject> GetCurrentLoadout()
    {
        return loadout;
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
