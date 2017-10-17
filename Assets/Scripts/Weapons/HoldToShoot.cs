using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HoldToShoot : AbstractProjectileWeapon {
    protected override void OnHold(GameObject holder)
    {
        base.OnHold(holder);
        if (cooldown <= 0 && holdTime >= shotDelay)
        {
            Shoot(holder);
            cooldown = shotInterval;
        }
    }

    [CustomEditor(typeof(HoldToShoot))]
    public class HoldToShootEditor : AbstractProjectileWeapon.AbstractProjectileWeaponEditor
    {
    }
}
