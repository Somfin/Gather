using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
