using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CassidyCombat : MonoBehaviour {
    public List<GameObject> loadout;
    public int equippedIndex;
    public Transform target;
    public Transform source;
	
	// Update is called once per frame
	void Update () {
        var currentWeaponItem = loadout.ElementAtOrDefault(equippedIndex);
        if (currentWeaponItem == null)
        {
            return;
        }
        var currentWeapon = currentWeaponItem.GetComponent<IWeapon>();
        currentWeapon.Ready(target.position, source.position, gameObject);
		if (Input.GetMouseButton(0))
        {
            currentWeapon.Pull(gameObject);
        } else
        {
            currentWeapon.Release(gameObject);
        }
	}
}
